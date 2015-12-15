using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JHSchool.Data;

namespace JHSchool.Permrec.StudentExtendControls.Ribbon
{
    public partial class TemporaStudentManager : UserControl
    {
        public TemporaStudentManager()
        {
            InitializeComponent();
        }

        private void txtClassName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && txtClassName.Text != "")
                foreach (JHClassRecord cr in JHClass.SelectAll ())
                    if (cr.Name == txtClassName.Text)
                        txtSeatNo.Focus();
        }

        private void txtClassName_TextChanged(object sender, EventArgs e)
        {
            bool pass = false, confuse = false;
            pictureBox1.Visible = false;
            txtSeatNo.Text = "";
            if (txtClassName.Text != "")
            {
                foreach (JHClassRecord cr in JHClass.SelectAll ())
                {
                    if (cr.Name == txtClassName.Text )
                    {
                        pictureBox1.Visible = true;
                        pass = true;
                    }
                    else if (cr.Name.StartsWith(txtClassName.Text))
                        confuse = true;
                }
                if (pass & confuse == false)
                    txtSeatNo.Focus();
            }
        }

        private void txtClassName_Enter(object sender, EventArgs e)
        {
            txtClassName.SelectAll();
        }

        private void txtSeatNo_Enter(object sender, EventArgs e)
        {
            txtSeatNo.SelectAll();
        }

        private void txtStudentNumber_Enter(object sender, EventArgs e)
        {
            txtStudentNumber.SelectAll();
        }

        private void txtStudentNumber_TextChanged(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
            if (txtStudentNumber.Text != "")
            {
                foreach (JHStudentRecord studRec in JHStudent.SelectAll())
                {
                    if (studRec.StudentNumber == txtStudentNumber.Text)
                        pictureBox3.Visible = true;
                }
            }
        }

        private void txtStudentNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && txtStudentNumber.Text != "")
            {
                foreach (JHStudentRecord studRec in JHStudent.SelectAll ())
                {
                    if (studRec.StudentNumber == txtStudentNumber.Text)
                    {                      
                        if (!K12.Presentation.NLDPanels.Student.TempSource.Contains(studRec.ID))
                        {
                            List<string> ids = new List<string>();
                            // 只加入一般或輟學
                            if (studRec.Status == K12.Data.StudentRecord.StudentStatus.一般 || studRec.Status == K12.Data.StudentRecord.StudentStatus.輟學)
                            {
                                ids.Add(studRec.ID);
                                K12.Presentation.NLDPanels.Student.AddToTemp(ids);
                            }
                        }
                        txtStudentNumber.SelectAll();
                        
                    }
                }
            }
        }

        private void txtSeatNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && txtSeatNo.Text !="")
            {
                foreach (JHClassRecord cr in JHClass.SelectAll ())
                {
                    if (cr.Name == txtClassName.Text)
                    {
                        foreach (JHStudentRecord studRec in JHStudent.SelectAll ())
                        {
                            // 只加入一般或輟學
                            if (studRec.Status == K12.Data.StudentRecord.StudentStatus.一般 || studRec.Status == K12.Data.StudentRecord.StudentStatus.輟學)
                            {
                                if (studRec.Class != null && studRec.SeatNo.HasValue)
                                    if (studRec.SeatNo.Value.ToString() == txtSeatNo.Text && studRec.Class.Name == txtClassName.Text)
                                    {
                                        if (!K12.Presentation.NLDPanels.Student.TempSource.Contains(studRec.ID))
                                        {
                                            List<string> ids = new List<string>();
                                            ids.Add(studRec.ID);
                                            K12.Presentation.NLDPanels.Student.AddToTemp(ids);
                                        }
                                        txtClassName.Focus();
                                    }
                            }
                        }
                    }
                }
            }
        }

        private void txtSeatNo_TextChanged(object sender, EventArgs e)
        {
            bool pass = false;
            pictureBox2.Visible = false;
            if (txtSeatNo.Text != "")
            {
                foreach (JHClassRecord cr in JHClass.SelectAll ())
                {
                    if (cr.Name == txtClassName.Text)
                    {
                        foreach (JHStudentRecord studRec in JHStudent.SelectAll())
                        {
                            if (studRec.Status == K12.Data.StudentRecord.StudentStatus.一般 || studRec.Status == K12.Data.StudentRecord.StudentStatus.輟學)
                            {
                                if (studRec.Class != null && studRec.SeatNo.HasValue)
                                    if (studRec.Class.Name == txtClassName.Text && studRec.SeatNo.ToString() == txtSeatNo.Text)
                                        pass = true;
                            }
                        }
                    }
                }
                if (pass)
                    pictureBox2.Visible = true;
            }
        }
    }
}
