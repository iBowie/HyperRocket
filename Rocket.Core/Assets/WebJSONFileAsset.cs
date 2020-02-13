using Newtonsoft.Json;
using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Core.Utils;
using System;
using System.IO;

namespace Rocket.Core.Assets
{
    public class WebJSONFileAsset<T> : Asset<T> where T : class, IDefaultable
    {
        private Uri url;
        private RocketWebClient webclient = new RocketWebClient();
        private System.Net.DownloadStringCompletedEventHandler handler = new System.Net.DownloadStringCompletedEventHandler((object sender, System.Net.DownloadStringCompletedEventArgs e) => { });
        private bool waiting = false;
        public WebJSONFileAsset(Uri url = null, AssetLoaded<T> callback = null)
        {
            this.url = url;
            Load(callback);
        }

        public override void Load(AssetLoaded<T> callback = null)
        {
            try
            {
                if (!waiting)
                {
                    Logger.Log($"Updating WebJSONFileAsset {typeof(T).Name} from {url}");
                    waiting = true;

                    webclient.DownloadStringCompleted -= handler;
                    handler = (object sender, System.Net.DownloadStringCompletedEventArgs e) =>
                    {
                        if (e.Error != null)
                        {
                            Logger.Log($"Error retrieving WebJSONFileAsset {typeof(T).Name} from {url}: {e.Error.Message}");
                        }
                        else
                        {
                            try
                            {
                                using (StringReader reader = new StringReader(e.Result))
                                {
                                    T result = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
                                    if (result != null)
                                    {
                                        TaskDispatcher.QueueOnMainThread(() =>
                                        {
                                            instance = result;
                                            Logger.Log($"Successfully updated WebJSONFileAsset {typeof(T).Name} from {url}");
                                        });
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.Log($"Error retrieving WebJSONFileAsset {typeof(T).Name} from {url}: {ex.Message}");
                            }
                        }

                        TaskDispatcher.QueueOnMainThread(() =>
                        {
                            callback?.Invoke(this);
                            waiting = false;
                        });
                    };
                    webclient.DownloadStringCompleted += handler;
                    webclient.DownloadStringAsync(url);
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Error retrieving WebJSONFileAsset {typeof(T).Name} from {url}: {ex.Message}");
            }
        }
    }
}
