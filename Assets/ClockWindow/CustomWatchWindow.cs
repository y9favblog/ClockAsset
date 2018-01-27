using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Timers;
using Mono.CSharp;

public class CustomWatchWindow : EditorWindow
{
    static private Texture[] tex_h = new Texture[2];
    static private Texture[] tex_m = new Texture[2];
    static private Texture[] tex_s = new Texture[2];
    static private Texture tex_dot = new Texture();
    static string hour = "00";
    static string min = "00";
    static string sec = "00";
    public static Texture[] tex = new Texture[10];
    static ClockImageData ClockSetting;
    static float pos_h;

    [InitializeOnLoadMethod]
    static void Start()
    {
        EditorApplication.update += EditorUpdate;
    }

    [MenuItem("Window/CustomWatchWindow")]
    private static void Open()
    {
        ClockSetting = Resources.Load<ClockImageData>("ClockSetting");
        for (int i = 0; i < 10; i++)
        {
            tex[i] = ClockSetting.Images[i];
        }
        tex_dot = ClockSetting.colon;
        EditorWindow.GetWindow<CustomWatchWindow>("CustomWatchWindow");
    }

    private void OnGUI()
    {
        for (int i = 0; i < 2; i++)
        {
            tex_h[i] = tex[(int)char.GetNumericValue(hour[i])];
            tex_m[i] = tex[(int)char.GetNumericValue(min[i])];
            tex_s[i] = tex[(int)char.GetNumericValue(sec[i])];
        }



            pos_h = this.position.height;
            View();
    }

    static void View(){
        //表示する画像のサイズ
        var textureWidth = (float)tex_h[0].width / 1.9f;
        var textureHeight = (float)tex_h[0].height / 1.9f;

        var dotWidth = (float)tex_dot.width / 1.9f;

        var posY = (pos_h - textureHeight) / 1.9f;
        var dot_w = tex_dot.width - 10;
        //hour
        EditorGUI.DrawTextureTransparent(new Rect(0, posY, textureWidth, textureHeight), tex_h[0]);
        EditorGUI.DrawTextureTransparent(new Rect(textureWidth, posY, textureWidth, textureHeight), tex_h[1]);
        //dot
        EditorGUI.DrawTextureTransparent(new Rect(textureWidth * 2, posY, dotWidth, textureHeight), tex_dot);
        //minute
        EditorGUI.DrawTextureTransparent(new Rect(textureWidth * 2 + dot_w, posY, textureWidth, textureHeight), tex_m[0]);
        EditorGUI.DrawTextureTransparent(new Rect(textureWidth * 3 + dot_w, posY, textureWidth, textureHeight), tex_m[1]);
        //dot
        EditorGUI.DrawTextureTransparent(new Rect(textureWidth * 4 + dot_w, posY, dotWidth, textureHeight), tex_dot);
        //sec
        EditorGUI.DrawTextureTransparent(new Rect(textureWidth * 4 + (dot_w * 2), posY, textureWidth, textureHeight), tex_s[0]);
        EditorGUI.DrawTextureTransparent(new Rect(textureWidth * 5 + (dot_w * 2), posY, textureWidth, textureHeight), tex_s[1]);
    }

    static void EditorUpdate() {
        var t = DateTime.Now;
        sec = t.Second.ToString();
        min = t.Minute.ToString();
        hour = t.Hour.ToString();

        if (hour.Length == 1){
            hour = "0" + hour[0];
        }
        if (min.Length == 1){
            min = "0" + min[0];
        }
        if (sec.Length == 1){
            sec = "0" + sec[0];
        }


    }
    private void OnInspectorUpdate()
    {
        Repaint();
    }
}