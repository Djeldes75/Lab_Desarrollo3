using System.Windows.Forms;

namespace Semana1_Donativos.Utils
{
    public enum NotifyType { Success, Info, Warning, Error }

    public static class Notifier
    {
        public static void Show(string message, NotifyType type)
        {
            string caption;
            MessageBoxIcon icon;

            switch (type)
            {
                case NotifyType.Success:
                    caption = "✓ Operación exitosa";
                    icon = MessageBoxIcon.Information;
                    break;
                case NotifyType.Info:
                    caption = "ℹ Información";
                    icon = MessageBoxIcon.Information;
                    break;
                case NotifyType.Warning:
                    caption = "⚠ Advertencia";
                    icon = MessageBoxIcon.Warning;
                    break;
                case NotifyType.Error:
                    caption = "✖ Error";
                    icon = MessageBoxIcon.Error;
                    break;
                default:
                    caption = "Mensaje";
                    icon = MessageBoxIcon.None;
                    break;
            }

            MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);
        }

        // atajos
        public static void Success(string msg) => Show(msg, NotifyType.Success);
        public static void Info(string msg) => Show(msg, NotifyType.Info);
        public static void Warning(string msg) => Show(msg, NotifyType.Warning);
        public static void Error(string msg) => Show(msg, NotifyType.Error);
    }
}
