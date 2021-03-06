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
        private DataTable Groups = new DataTable();
        private DataTable Users = new DataTable();
        private DataTable UserGroup = new DataTable();

        private string ConnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\vanka\source\repos\TestApp\TestApp\TestDB.mdf;Integrated Security=True";

        public MainForm()
        {
            InitializeComponent();

            FillDTs();
            
            GetDataForCbGroups();

            dgUsers.DataSource = Users;
            dgGroups.DataSource = Groups;
        }

        private void GroupModify()
        {
            if (Groups.Columns.Count < 3)
            {
                DataColumn qnt = new DataColumn();
                qnt.ColumnName = "Сотрудников в группе";
                Groups.Columns.Add(qnt);
            }

            foreach (DataRow row in Groups.Rows)
                row["Сотрудников в группе"] = GetQnt(row["ID_Group"]);
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
                {
                    string query = "ID_Group = " + row["ID_Group"].ToString();
                    string group_name = Groups.Select(query)[0]["Description"].ToString();
                    result += (group_name + ";");
                }

            return result;
        }

        private int GetQnt(object id_group)
        {
            int qnt = 0;

            foreach (DataRow row in UserGroup.Rows)
                if (row["ID_Group"].Equals(id_group))
                    qnt++;

            return qnt;
        }

        private void FillDTs()
        {
            FillDT(Groups, "SELECT * FROM Groups");
            FillDT(Users, "SELECT * FROM Users");
            FillDT(UserGroup, "SELECT * FROM UserGroup");

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

        private void ExecuteCmd(SqlCommand command)
        {
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        private void DeleteFromTable(int user_id, string table_name, SqlConnection connection)
        {
            //Адаптер в данной реализации выглядит каким-то лишним, но я начал делать с его помощью, в итоге он здесь остался
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                SqlCommand del_cmd = new SqlCommand("DELETE FROM " + table_name + " WHERE ID_User = @user_id", connection);
                del_cmd.Parameters.AddWithValue("@user_id", user_id);

                adapter.DeleteCommand = del_cmd;
                ExecuteCmd(adapter.DeleteCommand);
            }
        }

        private void DeleteUser(int user_id)
        {
            // Возможно, здесь должен быть другой способ вместо последовательного удаления из двух таблиц
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                DeleteFromTable(user_id, "UserGroup", connection);
                DeleteFromTable(user_id, "Users", connection);
            }
        }

        private void AddToUsers(string fio, SqlConnection connection)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Users (FIO)" + "VALUES(@FIO)", connection);
                cmd.Parameters.AddWithValue("@FIO", fio);

                adapter.InsertCommand = cmd;
                ExecuteCmd(adapter.InsertCommand);
            }

            FillDT(Users, "SELECT * FROM Users");
        }

        private void AddToUserGroup(int user_id, int group_id, SqlConnection connection)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO UserGroup (ID_User, ID_Group)"
                                                    + "VALUES(@id_user, @id_group)", connection);
                cmd.Parameters.AddWithValue("@id_user", user_id);
                cmd.Parameters.AddWithValue("@id_group", group_id);

                adapter.InsertCommand = cmd;
                ExecuteCmd(adapter.InsertCommand);
            }
        }

        private int CheckUserId(string fio)
        {
            foreach (DataRow row in Users.Select())
                if (row["FIO"].ToString() == fio)
                    return (int)row["ID_User"];

            return -1;
        }

        private void AddUser(string fio, string group)
        {
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                int user_id = CheckUserId(fio);

                if (user_id == -1)
                {
                    AddToUsers(fio, connection);
                    user_id = GetId(fio, Users, "FIO", "ID_User");
                }

                int group_id = GetId(group, Groups, "Description", "ID_Group");

                AddToUserGroup(user_id, group_id, connection);
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
