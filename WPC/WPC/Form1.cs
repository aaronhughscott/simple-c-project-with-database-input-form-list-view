using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WPC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

    

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            List<Employee> employeeList;
            try
            {
                employeeList = WPCDB.GetEmployee();
                if (employeeList.Count > 0)
                {
                    Employee employee;
                    for (int i = 0; i < employeeList.Count; i++)
                    {
                        employee = employeeList[i];
                        listView1.Items.Add(employee.EmployeeNumber.ToString());
                        listView1.Items[i].SubItems.Add(employee.FirstName);
                        listView1.Items[i].SubItems.Add(employee.LastName);
                        listView1.Items[i].SubItems.Add(employee.Department);
                        listView1.Items[i].SubItems.Add(employee.Phone);
                        listView1.Items[i].SubItems.Add(employee.Email);

                    }

                }
                else { MessageBox.Show("There are no employees.", "Alert"); }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString());  }
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            WPCDB.AddEmployee(txtFirstName.Text, txtLastName.Text, txtDepartment.Text, txtPhone.Text, txtEmail.Text);
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtDepartment.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            this.Form1_Load(this, null);
        }
    }
}
