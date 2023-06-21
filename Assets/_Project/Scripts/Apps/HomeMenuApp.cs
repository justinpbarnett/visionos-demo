public class HomeMenuApp : App
{
    public void StartApp(string appName)
    {
        AppManager.Instance.Load(appName);
        AppManager.Instance.Minimize(this);
    }
}