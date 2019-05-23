using System;
using Medibox.Model;
using Medibox.Presenter;
using Sanita.Utility.Encryption;
using Sanita.Utility.Logger;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using Sanita.Utility;
using Npgsql;

namespace Medibox
{
    public partial class FormConfigDatabase : FormBase
    {
        private const String TAG = "FormConfigDatabase";

        public FormConfigDatabase()
        {
            InitializeComponent();
            this.Translate();
            this.UpdateUI();
            base.DoInit();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.S:
                    btnOK_Click(this, null);
                    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Database_Load(object sender, EventArgs e)
        {
            local_txtServer.Text = MyVar.mMediboxSetting.DB_SERVER;
            local_txtDatabase.Text = MyVar.mMediboxSetting.DB_NAME;
            local_txtUser.Text = MyVar.mMediboxSetting.DB_USERID;
            local_txtPassword.Text = MyVar.mMediboxSetting.DB_USERPASSWORD;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            MyVar.mMediboxSetting.DB_SERVER = local_txtServer.Text;
            MyVar.mMediboxSetting.DB_NAME = local_txtDatabase.Text;
            MyVar.mMediboxSetting.DB_USERID = local_txtUser.Text;
            MyVar.mMediboxSetting.DB_USERPASSWORD = local_txtPassword.Text;

            //Save
            try
            {
                MediboxSetting mMediboxSetting = ObjectCopier.Clone(MyVar.mMediboxSetting);

                if (!mMediboxSetting.DB_ENCRIPT)
                {
                    mMediboxSetting.DB_ENCRIPT = true;
                    mMediboxSetting.DB_SERVER = CryptorEngine.Encrypt(mMediboxSetting.DB_SERVER, true);
                    mMediboxSetting.DB_USERID = CryptorEngine.Encrypt(mMediboxSetting.DB_USERID, true);
                    mMediboxSetting.DB_PORT = CryptorEngine.Encrypt(mMediboxSetting.DB_PORT, true);
                    mMediboxSetting.DB_USERPASSWORD = CryptorEngine.Encrypt(mMediboxSetting.DB_USERPASSWORD, true);
                    mMediboxSetting.DB_NAME = CryptorEngine.Encrypt(mMediboxSetting.DB_NAME, true);
                }

                File.WriteAllText(MyVar.SettingsFileName, JsonConvert.SerializeObject(mMediboxSetting));                
            }
            catch (Exception ex)
            {
                SanitaLog.e(TAG, ex);
            }

            SoftUpdatePresenter.SetConnectionConfig(local_txtServer.Text, local_txtUser.Text, local_txtPassword.Text, local_txtDatabase.Text, MyVar.DEFAULT_PORT);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnKiemTraKetNoi_Click(object sender, EventArgs e)
        {
            


            if (SoftUpdatePresenter.CheckConnection(local_txtServer.Text, local_txtUser.Text, local_txtPassword.Text, local_txtDatabase.Text, MyVar.DEFAULT_PORT))
            {
                SanitaMessageBox.Show("Kết nối cơ sở dữ liệu thành công !", "Thông Báo");
            }
            else
            {
                SanitaMessageBox.Show("Kết nối cơ sở dữ liệu không thành công !", "Thông Báo");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using(FormConfigSchema form = new FormConfigSchema())
            {
                form.ShowDialog();
            }
        }

        private void local_txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}