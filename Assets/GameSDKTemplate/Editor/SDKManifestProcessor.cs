using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class SDKManifestProcessor : IPreprocessBuildWithReport {

    private const string META_APPLICATION_ID = "com.google.android.gms.ads.APPLICATION_ID";
    private XNamespace ns = "http://schemas.android.com/apk/res/android";

    public int callbackOrder { get { return 0; } }

    public void OnPreprocessBuild(BuildReport report) {
        //string manifestPath = Path.Combine(
        //        Application.dataPath, "Plugins/Android/AndroidManifest.xml");

        //XDocument manifest = null;
        //try {
        //    manifest = XDocument.Load(manifestPath);
        //} catch (IOException e) {
        //    StopBuildWithMessage("AndroidManifest.xml is missing. Try re-importing the plugin.");
        //}
        //XElement elemManifest = manifest.Element("manifest");
        //if (elemManifest == null) {
        //    StopBuildWithMessage("AndroidManifest.xml is not valid. Try re-importing the plugin.");
        //}

        //XElement elemApplication = elemManifest.Element("application");
        //if (elemApplication == null) {
        //    StopBuildWithMessage("AndroidManifest.xml is not valid. Try re-importing the plugin.");
        //}
        //IEnumerable<XElement> metas = elemApplication.Descendants()
        //        .Where(elem => elem.Name.LocalName.Equals("meta-data"));

        //XElement elemAdMobEnabled = GetMetaElement(metas, META_APPLICATION_ID);
        //if (string.IsNullOrEmpty(GameSDKSettings.admobAppId)) {
        //    StopBuildWithMessage(
        //        "Android AdMob app ID is empty. Please enter a valid app ID to run ads properly.");
        //}
        //if (elemAdMobEnabled == null) {
        //    elemApplication.Add(CreateMetaElement(META_APPLICATION_ID, GameSDKSettings.admobAppId));
        //} else {
        //    elemAdMobEnabled.SetAttributeValue(ns + "value", GameSDKSettings.admobAppId);
        //}
        //elemManifest.Save(manifestPath);
    }

    private XElement GetMetaElement(IEnumerable<XElement> metas, string metaName) {
        foreach (XElement elem in metas) {
            IEnumerable<XAttribute> attrs = elem.Attributes();
            foreach (XAttribute attr in attrs) {
                if (attr.Name.Namespace.Equals(ns)
                        && attr.Name.LocalName.Equals("name") && attr.Value.Equals(metaName)) {
                    return elem;
                }
            }
        }
        return null;
    }

    private XElement CreateMetaElement(string name, object value) {
        return new XElement("meta-data",
                new XAttribute(ns + "name", name), new XAttribute(ns + "value", value));
    }

    private void StopBuildWithMessage(string message) {
        string prefix = "[GoogleMobileAds] ";
#if UNITY_2017_1_OR_NEWER
        throw new BuildPlayerWindow.BuildMethodException(prefix + message);
#else
        throw new OperationCanceledException(prefix + message);
#endif
    }
}
