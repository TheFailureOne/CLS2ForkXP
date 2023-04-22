using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogonShell
{
    public class EnterTextBox : TextBox
    {
        protected override bool IsInputKey(Keys key)
        {
            if (key == Keys.Enter)
                return true;
            return base.IsInputKey(key);
        }
    }
}
