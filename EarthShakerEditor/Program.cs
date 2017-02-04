using System;
using System.IO;
using System.Windows.Forms;
using EarthShakerEditor.Editor;
using EarthShakerEditor.Spectrum;

namespace EarthShakerEditor
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            EditorController controller = new EditorController(new EditorModel(), new EditorView());
            Application.Run(controller.View as Form);
        }
    }
}
