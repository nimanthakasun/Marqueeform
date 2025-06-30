using System.Text.Json.Nodes;
using System.Text.Json;
using RestSharp;
using System.ComponentModel;

namespace Marqueeform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OtherStuff randomstuff = new OtherStuff();
            Thread processingThread = new Thread(new ThreadStart(randomstuff.StartProcess));
            processingThread.Start();
            //processingThread.Join();
        }
    }
    public class MarqueeProgressForm : Form
    {
        public MarqueeProgressForm()
        {
            Text = "Please wait...";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(300, 80);
            ControlBox = false;

            var progressBar = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 30,
                Dock = DockStyle.Fill
            };

            Controls.Add(progressBar);
        }
    }

    public class OtherStuff
    {
        public void StartProcess()
        {
            var progressForm = new MarqueeProgressForm();
            var backgroundWorker = new BackgroundWorker();
            progressForm.Show();

            backgroundWorker.DoWork += (obj, e) => workerTasks(progressForm);
            backgroundWorker.RunWorkerCompleted += worker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();




            //await
            //    Task.Run(() =>
            //    { 
            //        try
            //        {
            //            // Simulate long work (replace with real logic)
            //            for(int i = 0; i < 10; i++)
            //            {
            //                var client = new RestClient("https://fake-json-api.mock.beeceptor.com");
            //                var request = new RestRequest("users");
            //                var response = client.ExecuteGet(request);
            //                var data = JsonSerializer.Deserialize<JsonNode>(response.Content!)!;
            //            }                    
            //        }
            //        catch(Exception)
            //        {
            //            MessageBox.Show("Exception thrown");
            //        }
            //        finally
            //        {
            //            // Safely close the progress form on the UI thread
            //            if (progressForm.InvokeRequired)
            //            {
            //                progressForm.Invoke(new System.Action(() => progressForm.Close()));
            //            }
            //            else
            //            {
            //                progressForm.Close();
            //            }

            //            // Optional: Notify user after closing
            //            MessageBox.Show("Process Complete!");

            //        }
            //    });
        }

        public void workerTasks(MarqueeProgressForm progressForm)
        {
            try
            {
                // Simulate long work (replace with real logic)
                for (int i = 0; i < 10; i++)
                {
                    var client = new RestClient("https://fake-json-api.mock.beeceptor.com");
                    var request = new RestRequest("users");
                    var response = client.ExecuteGet(request);
                    var data = JsonSerializer.Deserialize<JsonNode>(response.Content!)!;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Exception thrown");
            }
            finally
            {
                // Safely close the progress form on the UI thread
                if (progressForm.InvokeRequired)
                {
                    progressForm.Invoke(new System.Action(() => progressForm.Close()));
                }
                else
                {
                    progressForm.Close();
                }

                // Optional: Notify user after closing
                MessageBox.Show("Process Complete! from worker");

            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // will output "all done" to the console
            MessageBox.Show("Process Complete! from end task");
        }
    }
}
