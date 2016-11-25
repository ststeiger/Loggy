
namespace Loggy
{

    // https://stackoverflow.com/questions/29375020/how-can-i-stop-iisexpress-from-dumping-every-detail-of-every-request-into-the-vi
    // https://stackoverflow.com/questions/37103660/asp-net-core-iis-express-how-to-view-log-mesages
    class DebugTextWriter : System.IO.TextWriter
    {
        public override void Write(char[] buffer, int index, int count)
        {
            System.Diagnostics.Debug.Write(new string(buffer, index, count));
        }


        public override void Write(string value)
        {
            System.Diagnostics.Debug.Write(value);
        }

        public override System.Text.Encoding Encoding
        {
            get { return System.Text.Encoding.Default; }
        }
    }


    public class MultiTextWriter : System.IO.TextWriter
    {
        private System.Collections.Generic.IEnumerable<System.IO.TextWriter> writers;

        public MultiTextWriter(System.Collections.Generic.IEnumerable<System.IO.TextWriter> writers)
        {
            this.writers = writers;
        }

        public MultiTextWriter(params System.IO.TextWriter[] writers)
        {
            this.writers = writers;
        }


        public override void Write(char value)
        {
            foreach (var writer in writers)
                writer.Write(value);
        }

        public override void Write(string value)
        {
            foreach (var writer in writers)
                writer.Write(value);
        }

        public override void Flush()
        {
            foreach (var writer in writers)
                writer.Flush();
        }

        public override void Close()
        {
            foreach (var writer in writers)
                writer.Close();
        }

        public override System.Text.Encoding Encoding
        {
            get { return System.Text.Encoding.Default; }
        }
    }


}