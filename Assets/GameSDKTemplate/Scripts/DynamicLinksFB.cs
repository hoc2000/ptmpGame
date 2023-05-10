//using Firebase.DynamicLinks;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;
//using UnityEngine;
//using UnityEngine.Networking;
//using UnityEngine.UI;

//public class DynamicLinksFB : MonoBehaviour {
//    // Start is called before the first frame update

//    void Start() {
//        DynamicLinks.DynamicLinkReceived += DynamicLinks_DynamicLinkReceived;
//    }
//    private void DynamicLinks_DynamicLinkReceived(object sender, ReceivedDynamicLinkEventArgs e) {
//        var dynamicLinkEventArgs = e;
//        //string codeLinks = dynamicLinkEventArgs.ReceivedDynamicLink.Url.AbsoluteUri.Split('=')[1
//        Dictionary<string, string> queryParams = ParseQueryString(dynamicLinkEventArgs.ReceivedDynamicLink.Url.Query);
//        if (queryParams != null && queryParams.ContainsKey("event_code")) {
//            string codeLinks = queryParams["event_code"];
//            Debug.LogFormat("Received dynamic link {0}",
//                codeLinks);
//            if (!SDKPlayPrefs.GetDynamicLinksClaim(codeLinks)) {
//                ReciveGift(codeLinks);
//                SDKPlayPrefs.SetDynamicLinksClaim(codeLinks, true);
//            }
//        }
//    }
//    // Display the dynamic link received by the application.
//    void ReciveGift(string code) {
//        ResourceItem[] resources = GiftCodeData.Instance.GetGift(code);
//        if (resources != null) {
//            GameData.ClaimResourceItems(resources);
//            PopupReciveGift.Setup(resources).Show();
//            GameData.firstReciveGiftCode = true;
//        } else {
//            PopupCodeNotValid.Setup().Show();
//        }
//    }
//    private Dictionary<string, string> ParseQueryString(string query) {
//        if (query.Length == 0)
//            return null;
//        Dictionary<string, string> result = new Dictionary<string, string>();
//        Encoding encoding = Encoding.UTF8;
//        var decodedLength = query.Length;
//        var namePos = 0;
//        var first = true;

//        while (namePos <= decodedLength) {
//            int valuePos = -1, valueEnd = -1;
//            for (var q = namePos; q < decodedLength; q++) {
//                if ((valuePos == -1) && (query[q] == '=')) {
//                    valuePos = q + 1;
//                } else if (query[q] == '&') {
//                    valueEnd = q;
//                    break;
//                }
//            }

//            if (first) {
//                first = false;
//                if (query[namePos] == '?')
//                    namePos++;
//            }

//            string name;
//            if (valuePos == -1) {
//                name = null;
//                valuePos = namePos;
//            } else {
//                name = UnityWebRequest.UnEscapeURL(query.Substring(namePos, valuePos - namePos - 1), encoding);
//            }
//            if (valueEnd < 0) {
//                namePos = -1;
//                valueEnd = query.Length;
//            } else {
//                namePos = valueEnd + 1;
//            }
//            var value = UnityWebRequest.UnEscapeURL(query.Substring(valuePos, valueEnd - valuePos), encoding);

//            result.Add(name, value);
//            if (namePos == -1)
//                break;
//        }
//        return result;
//    }

//}
