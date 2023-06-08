#if UNITY_IPHONE || UNITY_IOS

using System.Collections.Generic;
using System.IO;

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;
public static class SDKPListProcessor
{
    private static string[] ORIENTATIONS = new string[] { "UIInterfaceOrientationLandscapeRight", "UIInterfaceOrientationLandscapeLeft", "UIInterfaceOrientationPortraitUpsideDown", "UIInterfaceOrientationPortrait" };
    private static string[] adNetworkIdentifiers = new string[] {
"su67r6k2v3.skadnetwork",
"f7s53z58qe.skadnetwork",
"2u9pt9hc89.skadnetwork",
"hs6bdukanm.skadnetwork",
"8s468mfl3y.skadnetwork",
"c6k4g5qg8m.skadnetwork",
"v72qych5uu.skadnetwork",
"44jx6755aq.skadnetwork",
"prcb7njmu6.skadnetwork",
"m8dbw4sv7c.skadnetwork",
"3rd42ekr43.skadnetwork",
"4fzdc2evr5.skadnetwork",
"t38b2kh725.skadnetwork",
"f38h382jlk.skadnetwork",
"424m5254lk.skadnetwork",
"ppxm28t8ap.skadnetwork",
"av6w8kgt66.skadnetwork",
"4pfyvq9l8r.skadnetwork",
"yclnxrl5pm.skadnetwork",
"tl55sbb4fm.skadnetwork",
"mlmmfzh3r3.skadnetwork",
"klf5c3l5u5.skadnetwork",
"9t245vhmpl.skadnetwork",
"9rd848q2bz.skadnetwork",
"7ug5zh24hu.skadnetwork",
"4468km3ulz.skadnetwork",
"7rz58n8ntl.skadnetwork",
"ejvt5qm6ak.skadnetwork",
"5lm9lj6jb7.skadnetwork",
"mtkv5xtk9e.skadnetwork",
"6g9af3uyq4.skadnetwork",
"uw77j35x4d.skadnetwork",
"u679fj5vs4.skadnetwork",
"rx5hdcabgc.skadnetwork",
"g28c52eehv.skadnetwork",
"cg4yq2srnc.skadnetwork",
"9nlqeag3gk.skadnetwork",
"275upjj5gd.skadnetwork",
"wg4vff78zm.skadnetwork",
"qqp299437r.skadnetwork",
"2fnua5tdw4.skadnetwork",
"3qcr597p9d.skadnetwork",
"3qy4746246.skadnetwork",
"3sh42y64q3.skadnetwork",
"5a6flpkh64.skadnetwork",
"cstr6suwn9.skadnetwork",
"e5fvkxwrpn.skadnetwork",
"kbd757ywx3.skadnetwork",
"n6fk4nfna4.skadnetwork",
"p78axxw29g.skadnetwork",
"s39g8k73mm.skadnetwork",
"wzmmz9fp6w.skadnetwork",
"ydx93a7ass.skadnetwork",
"zq492l623r.skadnetwork",
"24t9a8vw3c.skadnetwork",
"32z4fx6l9h.skadnetwork",
"523jb4fst2.skadnetwork",
"54nzkqm89y.skadnetwork",
"578prtvx9j.skadnetwork",
"5l3tpt7t6e.skadnetwork",
"6xzpu9s2p8.skadnetwork",
"79pbpufp6p.skadnetwork",
"9b89h5y424.skadnetwork",
"cj5566h2ga.skadnetwork",
"feyaarzu9v.skadnetwork",
"ggvn48r87g.skadnetwork",
"glqzh8vgby.skadnetwork",
"gta9lk7p23.skadnetwork",
"k674qkevps.skadnetwork",
"ludvb6z3bs.skadnetwork",
"n9x2a789qt.skadnetwork",
"pwa73g5rt2.skadnetwork",
"xy9t38ct57.skadnetwork",
"zmvfpc5aq8.skadnetwork",
"22mmun2rn5.skadnetwork",
"294l99pt4k.skadnetwork",
"44n7hlldy6.skadnetwork",
"4dzt52r2t5.skadnetwork",
"4w7y6s5ca2.skadnetwork",
"5tjdwbrq8w.skadnetwork",
"6964rsfnh4.skadnetwork",
"6p4ks3rnbw.skadnetwork",
"737z793b9f.skadnetwork",
"74b6s63p6l.skadnetwork",
"84993kbrcf.skadnetwork",
"97r2b46745.skadnetwork",
"a7xqa6mtl2.skadnetwork",
"b9bk5wbcq9.skadnetwork",
"bxvub5ada5.skadnetwork",
"dzg6xy7pwj.skadnetwork",
"f73kdq92p3.skadnetwork",
"g2y4y55b64.skadnetwork",
"hdw39hrw9y.skadnetwork",
"kbmxgpxpgc.skadnetwork",
"lr83yxwka7.skadnetwork",
"mls7yz5dvl.skadnetwork",
"mp6xlyr22a.skadnetwork",
"pwdxu55a5a.skadnetwork",
"r45fhb6rf7.skadnetwork",
"rvh3l7un93.skadnetwork",
"s69wq72ugq.skadnetwork",
"w9q455wk68.skadnetwork",
"x44k69ngh6.skadnetwork",
"x8uqf25wch.skadnetwork",
"y45688jllp.skadnetwork",
"v9wttpbfk9.skadnetwork",
"n38lu8286q.skadnetwork",
"252b5q8x7y.skadnetwork",
"9g2aggbj52.skadnetwork",
"wzmmZ9fp6w.skadnetwork",
"nu4557a4je.skadnetwork",
"v4nxqhlyqp.skadnetwork",
"r26jy69rpl.skadnetwork",
"238da6jt44.skadnetwork",
"3l6bd9hu43.skadnetwork",
"488r3q3dtq.skadnetwork",
"52fl2v3hgk.skadnetwork",
"6v7lgmsu45.skadnetwork",
"89z7zv988g.skadnetwork",
"8m87ys6875.skadnetwork",
"hb56zgv37p.skadnetwork",
"m297p6643m.skadnetwork",
"m5mvw97r93.skadnetwork",
"vcra2ehyfk.skadnetwork",
"9yg77x724h.skadnetwork",
"ecpz2srf59.skadnetwork",
"gvmwg8q7h5.skadnetwork",
"n66cz3y3bx.skadnetwork",
"nzq8sh4pbs.skadnetwork",
"pu4na253f3.skadnetwork",
"v79kvwwj4g.skadnetwork",
"yrqqpx2mcb.skadnetwork",
"z4gj7hsk7h.skadnetwork",
"7953jerfzd.skadnetwork"};
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string path)
    {
#if UNITY_IPHONE || UNITY_IOS
        string plistPath = Path.Combine(path, "Info.plist");
        PlistDocument plist = new PlistDocument();
        plist.ReadFromFile(plistPath);
        plist.root.SetBoolean("FirebaseAppStoreReceiptURLCheckEnabled", false);
        plist.root.SetString("NSAdvertisingAttributionReportEndpoint", "https://postbacks-is.com");
        var networkItems = plist.root.CreateArray("SKAdNetworkItems");
        for (int i = 0; i < adNetworkIdentifiers.Length; i++) {
            var networkItemDict = networkItems.AddDict();
            networkItemDict.SetString("SKAdNetworkIdentifier", adNetworkIdentifiers[i]);
        }
        plist.root.SetString("NSUserTrackingUsageDescription", "This identifier will be used to personalized your advertising experience.");
        File.WriteAllText(plistPath, plist.WriteToString());
#endif
    }
}

#endif
