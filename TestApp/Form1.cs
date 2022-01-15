using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    public partial class MainForm : Form
    {
        DataTable Groups = new DataTable();
        DataTable Users = new DataTable();
        DataTable UserGroup = new DataTable();

        string ConnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\vanka\source\repos\TestApp\TestApp\TestDB.mdf;Integrated Security=True";

        public MainForm()
        {
            InitializeComponent();

            FillDTs();
            
            GetDataForCbGroups();
            GetDataForDgGroups();
            GetDataForDgUsers();
        }

        private void GroupModify()
        {
            if (Groups.Columns.Count < 3)
            {
                DataColumn qnt = new DataColumn();
                qnt.ColumnName = "Qnt";
                Groups.Columns.Add(qnt);
            }

            foreach (DataRow row in Groups.Rows)
                row["Qnt"] = GetQnt(row["ID_Group"]);
        }

        private void UserModify()
        {
            if (Users.Columns.Count < 3)
            {
                DataColumn list = new DataColumn();
                list.ColumnName = "Group List";
                Users.Columns.Add(list);
            }

            foreach (DataRow row in Users.Rows)
                row["Group List"] = GetList(row["ID_User"]);
        }

        private string GetList(object id_user)
        {
            string result = "";

            foreach (DataRow row in UserGroup.Rows)
                if (row["ID_User"].Equals(id_user))
                    result += (Groups.Select ("ID_Group = " + row["ID_Group"].ToString()) [0]["Description"].ToString() + ";");

            return result;
        }

        private void GetDataForDgUsers()
        {
            dgUsers.DataSource = Users;
        }

        private void GetDataForDgGroups()
        {
            dgGroups.DataSource = Groups;
        }

        private int GetQnt(object id_group)
        {
            int result = 0;

            foreach (DataRow row in UserGroup.Rows)
                if (row["ID_Group"].Equals(id_group))
                    result++;

            return result;
        }

        private void FillDTs()
        {
            string query_groups = "SELECT * FROM Groups";
            string query_users = "SELECT * FROM Users";
            string query_user_group = "SELECT * FROM UserGroup";

            FillDT(Groups, query_groups);
            FillDT(Users, query_users);
            FillDT(UserGroup, query_user_group);

            GroupModify();
            UserModify();
        }
        private void FillDT(DataTable table, string query)
        {
            table.Rows.Clear();

            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(table);
                adapter.Dispose();
            }

        }

        private void GetDataForCbGroups()
        {
            cbGroups.DataSource = Groups;
            cbGroups.DisplayMember = "Description";
            cbGroups.ValueMember = "Description";
        }

        private void dgUsers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int row_index = dgUsers.HitTest(e.X, e.Y).RowIndex;
                int user_id = (int)dgUsers.Rows[row_index].Cells[0].Value;

                DeleteUser(user_id);

                FillDTs();
            }
        }

        private void ExecuteDel(SqlDataAdapter adapter, SqlConnection connection)
        {

            //Странный способ, похож на какой-то костыль. Так делал чувак на Youtube

            connection.Open();
            adapter.DeleteCommand.ExecuteNonQuery();
            connection.Close();
        }

        private void DeleteUser(int user_id)
        {
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();

                // PROBABLY, JOIN IS REQUIRED HERE
                SqlCommand ug_del_cmd = new SqlCommand("DELETE FROM UserGroup WHERE ID_User = @user_id", connection);
                ug_del_cmd.Parameters.Add("@user_id", SqlDbType.Int);
                ug_del_cmd.Parameters["@user_id"].Value = user_id;
                
                adapter.DeleteCommand = ug_del_cmd;
                ExecuteDel(adapter, connection);

                SqlCommand u_del_cmd = new SqlCommand("DELETE FROM Users WHERE ID_User = @user_id", connection);
                u_del_cmd.Parameters.Add("@user_id", SqlDbType.Int);
                u_del_cmd.Parameters["@user_id"].Value = user_id;

                adapter.DeleteCommand = u_del_cmd;
                ExecuteDel(adapter, connection);

                adapter.Dispose();
            }
        }

        private void ExecuteAdd(SqlDataAdapter adapter, SqlConnection connection)
        {

            //Странный способ, похож на какой-то костыль. Так делал чувак на Youtube

            connection.Open();
            adapter.InsertCommand.ExecuteNonQuery();
            connection.Close();
        }

        private void AddUser(string fio, string group)
        {
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();

                SqlCommand u_cmd = new SqlCommand("INSERT INTO Users (FIO)" + "VALUES(@FIO)", connection);
                u_cmd.Parameters.Add("@FIO", SqlDbType.NVarChar);
                u_cmd.Parameters["@FIO"].Value = fio;

                adapter.InsertCommand = u_cmd;
                ExecuteAdd(adapter, connection);
                FillDT(Users, "SELECT * FROM Users");

                int group_id = GetId(group, Groups, "Description", "ID_Group");
                int user_id = GetId(fio, Users, "FIO", "ID_User");
                
                SqlCommand ug_cmd = new SqlCommand("INSERT INTO UserGroup (ID_User, ID_Group)"
                                                    + "VALUES(@id_user, @id_group)", connection);
                ug_cmd.Parameters.Add("@id_user", SqlDbType.Int);
                ug_cmd.Parameters.Add("@id_group", SqlDbType.Int);
                ug_cmd.Parameters["@id_user"].Value = user_id;
                ug_cmd.Parameters["@id_group"].Value = group_id;

                adapter.InsertCommand = ug_cmd;
                ExecuteAdd(adapter, connection);

                adapter.Dispose();
            }
        }

        private int GetId(string group, DataTable table, string src, string dst)
        {
            int result = 0;
            DataRow[] rows = table.Select();

            for (int i = 0; i < rows.Length; i++)
                if (rows[i][src].ToString() == group)
                    result = (int)rows[i][dst];

            return result;
        }

        private void bttAdd_Click(object sender, EventArgs e)
        {
            string fio = tbFio.Text;
            string group = cbGroups.SelectedValue.ToString();

            AddUser(fio, group);

            FillDTs();
        }
    }
}
