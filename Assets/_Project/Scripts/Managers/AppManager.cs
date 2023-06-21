using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AppManager : Singleton<AppManager>
{
    [Header("Settings")]
    [SerializeField] private string homeMenuName = "Home Menu";
    
    private List<App> _loadedApps;

    private new void Awake()
    {
        base.Awake();
        Instance._loadedApps = new List<App>();
    }

    private void Start()
    {
        Instance.LoadHome();
    }

    public void Load(string appName)
    {
        App app;
        if (IsLoaded(appName))
        {
            app = Instance._loadedApps.First(a => a.gameObject.name.Contains(appName));
        }
        else
        {
            app = InstantiateApp(appName);
            Instance._loadedApps.Add(app);
        }
        app.Show();
    }

    public void Load(App app)
    {
        Instance.Load(app.gameObject.name);
    }

    public void LoadHome()
    {
        Instance.Load(homeMenuName);
    }

    public void Minimize(App app)
    {
        if (!IsLoaded(app)) return;
        app.Hide();
    }

    public void MinimizeAll()
    {
        foreach (App loadedApp in _loadedApps)
        {
            if (loadedApp.gameObject.name.Contains(homeMenuName)) continue;
            Instance.Minimize(loadedApp);
        }
        Instance.LoadHome();
    }

    public void Unload(App app)
    {
        if (!IsLoaded(app)) return;
        app.Hide();
        Instance._loadedApps.Remove(app);
        if (Instance._loadedApps.Count > 0) return;
        Instance.LoadHome();
    }

    public void UnloadAll()
    {
        foreach (App loadedApp in _loadedApps)
        {
            Instance.Unload(loadedApp);
        }
    }

    private static bool IsLoaded(App app) => Instance._loadedApps.Contains(app);
    private static bool IsLoaded(string appName) => Instance._loadedApps.Any(app => app.gameObject.name.Contains(appName));

    private static App InstantiateApp(string appName)
    {
        GameObject appObject = Instantiate(Resources.Load<GameObject>($"Apps/{appName}"));
        var app = appObject.GetComponent<App>();
        return app;
    }
}
