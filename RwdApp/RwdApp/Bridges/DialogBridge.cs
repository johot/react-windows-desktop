using Ookii.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwdApp
{
	public class DialogBridge
	{
		public string ShowDialog(string title, string msg, bool showCancel)
		{
			using (TaskDialog dialog = new TaskDialog())
			{
				dialog.WindowTitle = title;
				dialog.MainInstruction = title;
				dialog.Content = msg;

				TaskDialogButton okButton = new TaskDialogButton(ButtonType.Ok);			
				dialog.Buttons.Add(okButton);

				if (showCancel) {
					TaskDialogButton cancelButton = new TaskDialogButton(ButtonType.Cancel);
					dialog.Buttons.Add(cancelButton);
				}

				TaskDialogButton button = dialog.ShowDialog();

				return button.ButtonType.ToString();
			}
		}
	}
}
