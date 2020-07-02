using Aliyun.Acs.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;

namespace AliyunLiveGenerate
{
    class LiveGenerate
    {

        public static string generate_push_url(String pushDomain, String pushKey, long expireTime = 36000, String appName = "myteno", String streamName = "live")
        {
            string pushURL = "";
            if (pushKey == "")
            {
                pushURL = "rtmp://" + pushDomain + "/" + appName + "/" + streamName;
            }
            else
            {
                long timeStamp = DateTime.Now.currentTimeMillis() / 1000L + expireTime;
                String stringToMd5 = "/" + appName + "/" + streamName + "-" + timeStamp.ToString() + "-0-0-" + pushKey;
                String authKey = MD5C(stringToMd5);
                pushURL = "rtmp://" + pushDomain + "/" + appName + "/" + streamName + "?auth_key=" + timeStamp.ToString() + "-0-0-" + authKey;
            }
            return pushURL;
        }

        public static string MD5C(string str)
        {
            if (str == "" || str.Length == 0)
            {
                return null;
            }
            else
            {
                MD5 md5 = MD5.Create();

               


                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));


                var str1 = BitConverter.ToString(result).Replace("-", "").ToLower();



                //for (int i = 0; i < result.Length; i++)
                //{
                //    // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                //    str = str + result[i].ToString("x2");
                //}

                //while(str.Length<32)
                //{
                //    str = "0" + str;
                //}
                return str1;
            }
        }


        public static string general_pull_url(String pullDomain, String pullKey, String appName = "myteno", String streamName = "live", long expireTime = 36000)
        {
            String rtmpUrl = ""; //rtmp的拉流地址
            String hlsUrl = "";  //m3u8的拉流地址
            String flvUrl = "";  //flv的拉流地址


            //播放域名未配置鉴权Key的情况下
            if (pullKey == "")
            {
                rtmpUrl = "rtmp://" + pullDomain + "/" + appName + "/" + streamName;
                hlsUrl = "http://" + pullDomain + "/" + appName + "/" + streamName + ".m3u8";
                flvUrl = "http://" + pullDomain + "/" + appName + "/" + streamName + ".flv";
            }
            else
            {
                long timeStamp = DateTime.Now.currentTimeMillis() / 1000L + expireTime;
                String rtmpToMd5 = "/" + appName + "/" + streamName + "-" + timeStamp.ToString() + "-0-0-" + pullKey;
                String rtmpAuthKey = MD5C(rtmpToMd5);
                rtmpUrl = "rtmp://" + pullDomain + "/" + appName + "/" + streamName + "?auth_key=" + timeStamp.ToString() + "-0-0-" + rtmpAuthKey;

                String hlsToMd5 = "/" + appName + "/" + streamName + ".m3u8-" + timeStamp.ToString() + "-0-0-" + pullKey;
                String hlsAuthKey = MD5C(hlsToMd5);
                hlsUrl = "http://" + pullDomain + "/" + appName + "/" + streamName + ".m3u8" + "?auth_key=" + timeStamp.ToString() + "-0-0-" + hlsAuthKey;

                String flvToMd5 = "/" + appName + "/" + streamName + ".flv-" + timeStamp.ToString() + "-0-0-" + pullKey;
                String flvAuthKey = MD5C(flvToMd5);
                flvUrl = "http://" + pullDomain + "/" + appName + "/" + streamName + ".flv" + "?auth_key=" + timeStamp.ToString() + "-0-0-" + flvAuthKey;
            }

            return hlsUrl;

        }
    }
}
