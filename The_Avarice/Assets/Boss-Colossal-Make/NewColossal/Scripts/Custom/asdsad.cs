// 에디터 코드
using static UnityEngine.GraphicsBuffer;
using static XNodeEditor.NodeEditor;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

 
[CustomNodeEditor(typeof(AwakeNode))]
public class AwakeNodePlay : NodeEditor
{
    bool showAnimation = false;
    private float frameTimer;
    private int currentFrame;
    private double lastTime;

    public override void OnBodyGUI()
    {
        base.OnBodyGUI();
        if (GUILayout.Button("Toggle Animation"))
        {
            showAnimation = !showAnimation;
            // Repaint 강제 호출하면 즉시 반영됨
            NodeEditorWindow.current.Repaint();
        }

        if (!showAnimation)
        {
            // 애니메이션을 그리지 않고 종료
            return;
        }
        var node = target as AwakeNode;

        if (node.sprites == null || node.sprites.Length == 0)
            return;

        // 시간 계산
        double time = EditorApplication.timeSinceStartup;
        if (time - lastTime > 0.1f) // 0.1초마다 다음 프레임
        {
            currentFrame = (currentFrame + 1) % node.sprites.Length;
            lastTime = time;
        }
        Rect rect = GUILayoutUtility.GetRect(150, 150);
        Texture2D texture = AssetPreview.GetAssetPreview(node.sprites[currentFrame]);
        if (texture != null)
        {
            texture.filterMode = FilterMode.Point;

           
            GUI.DrawTexture(rect, texture, ScaleMode.ScaleToFit);
            //GUILayout.Label(texture, GUILayout.Width(100), GUILayout.Height(100));
        }
    }
}

[CustomNodeEditor(typeof(IdleNode))]
public class IdleNodePlay : NodeEditor
{
    bool showAnimation = false;
    private float frameTimer;
    private int currentFrame;
    private double lastTime;

    public override void OnBodyGUI()
    {
        base.OnBodyGUI();
        if (GUILayout.Button("Toggle Animation"))
        {
            showAnimation = !showAnimation;
            // Repaint 강제 호출하면 즉시 반영됨
            NodeEditorWindow.current.Repaint();
        }

        if (!showAnimation)
        {
            // 애니메이션을 그리지 않고 종료
            return;
        }
        var node = target as IdleNode;

        if (node.sprites == null || node.sprites.Length == 0)
            return;

        // 시간 계산
        double time = EditorApplication.timeSinceStartup;
        if (time - lastTime > 0.1f) // 0.1초마다 다음 프레임
        {
            currentFrame = (currentFrame + 1) % node.sprites.Length;
            lastTime = time;
        }
        Rect rect = GUILayoutUtility.GetRect(150, 150);
        Texture2D texture = AssetPreview.GetAssetPreview(node.sprites[currentFrame]);
        if (texture != null)
        {
            texture.filterMode = FilterMode.Point;

           
            GUI.DrawTexture(rect, texture, ScaleMode.ScaleToFit);
            //GUILayout.Label(texture, GUILayout.Width(100), GUILayout.Height(100));
        }
    }
}
[CustomNodeEditor(typeof(Dead))]
public class DeadPlay : NodeEditor
{
    bool showAnimation = false;
    private float frameTimer;
    private int currentFrame;
    private double lastTime;

    public override void OnBodyGUI()
    {
        base.OnBodyGUI();
        if (GUILayout.Button("Toggle Animation"))
        {
            showAnimation = !showAnimation;
            // Repaint 강제 호출하면 즉시 반영됨
            NodeEditorWindow.current.Repaint();
        }

        if (!showAnimation)
        {
            // 애니메이션을 그리지 않고 종료
            return;
        }
        var node = target as Dead;

        if (node.sprites == null || node.sprites.Length == 0)
            return;

        // 시간 계산
        double time = EditorApplication.timeSinceStartup;
        if (time - lastTime > 0.1f) // 0.1초마다 다음 프레임
        {
            currentFrame = (currentFrame + 1) % node.sprites.Length;
            lastTime = time;
        }
        Rect rect = GUILayoutUtility.GetRect(150, 150);
        Texture2D texture = AssetPreview.GetAssetPreview(node.sprites[currentFrame]);
        if (texture != null)
        {
            texture.filterMode = FilterMode.Point;

          
            GUI.DrawTexture(rect, texture, ScaleMode.ScaleToFit);
            //GUILayout.Label(texture, GUILayout.Width(100), GUILayout.Height(100));
        }
    }
}
[CustomNodeEditor(typeof(ChaseNode))]
public class ChaseNodePlay : NodeEditor
{
    bool showAnimation = false;
    private float frameTimer;
    private int currentFrame;
    private double lastTime;

    public override void OnBodyGUI()
    {
        base.OnBodyGUI();
        if (GUILayout.Button("Toggle Animation"))
        {
            showAnimation = !showAnimation;
            // Repaint 강제 호출하면 즉시 반영됨
            NodeEditorWindow.current.Repaint();
        }

        if (!showAnimation)
        {
            // 애니메이션을 그리지 않고 종료
            return;
        }
        var node = target as ChaseNode;

        if (node.sprites == null || node.sprites.Length == 0)
            return;

        // 시간 계산
        double time = EditorApplication.timeSinceStartup;
        if (time - lastTime > 0.1f) // 0.1초마다 다음 프레임
        {
            currentFrame = (currentFrame + 1) % node.sprites.Length;
            lastTime = time;
        }
        Rect rect = GUILayoutUtility.GetRect(150, 150);
        Texture2D texture = AssetPreview.GetAssetPreview(node.sprites[currentFrame]);
        if (texture != null)
        {
            texture.filterMode = FilterMode.Point;

           
            GUI.DrawTexture(rect, texture, ScaleMode.ScaleToFit);
            //GUILayout.Label(texture, GUILayout.Width(100), GUILayout.Height(100));
        }
    }
}
[CustomNodeEditor(typeof(slamDown))]
public class slamDownNodePlay : NodeEditor
{
    bool showAnimation = false;
    private float frameTimer;
    private int currentFrame;
    private double lastTime;

    public override void OnBodyGUI()
    {
        base.OnBodyGUI();
        if (GUILayout.Button("Toggle Animation"))
        {
            showAnimation = !showAnimation;
            // Repaint 강제 호출하면 즉시 반영됨
            NodeEditorWindow.current.Repaint();
        }

        if (!showAnimation)
        {
            // 애니메이션을 그리지 않고 종료
            return;
        }
        var node = target as slamDown;

        if (node.sprites == null || node.sprites.Length == 0)
            return;

        // 시간 계산
        double time = EditorApplication.timeSinceStartup;
        if (time - lastTime > 0.1f) // 0.1초마다 다음 프레임
        {
            currentFrame = (currentFrame + 1) % node.sprites.Length;
            lastTime = time;
        }
        Rect rect = GUILayoutUtility.GetRect(150, 150);
        Texture2D texture = AssetPreview.GetAssetPreview(node.sprites[currentFrame]);
        if (texture != null)
        {
            texture.filterMode = FilterMode.Point;

          
            GUI.DrawTexture(rect, texture, ScaleMode.ScaleToFit);
            //GUILayout.Label(texture, GUILayout.Width(100), GUILayout.Height(100));
        }
    }
}
[CustomNodeEditor(typeof(spinAttackNode))]
public class spinAttackNodePlay : NodeEditor
{
    bool showAnimation = false;
    private float frameTimer;
    private int currentFrame;
    private double lastTime;

    public override void OnBodyGUI()
    {
        base.OnBodyGUI();
        if (GUILayout.Button("Toggle Animation"))
        {
            showAnimation = !showAnimation;
            // Repaint 강제 호출하면 즉시 반영됨
            NodeEditorWindow.current.Repaint();
        }

        if (!showAnimation)
        {
            // 애니메이션을 그리지 않고 종료
            return;
        }
        var node = target as spinAttackNode;

        if (node.sprites == null || node.sprites.Length == 0)
            return;

        // 시간 계산
        double time = EditorApplication.timeSinceStartup;
        if (time - lastTime > 0.1f) // 0.1초마다 다음 프레임
        {
            currentFrame = (currentFrame + 1) % node.sprites.Length;
            lastTime = time;
        }
        Rect rect = GUILayoutUtility.GetRect(150, 150);
        Texture2D texture = AssetPreview.GetAssetPreview(node.sprites[currentFrame]);
        if (texture != null)
        {
            texture.filterMode = FilterMode.Point;

           
            GUI.DrawTexture(rect, texture, ScaleMode.ScaleToFit);
            //GUILayout.Label(texture, GUILayout.Width(100), GUILayout.Height(100));
        }
    }
}
[CustomNodeEditor(typeof(blowing))]
public class blowingPlay : NodeEditor
{
    bool showAnimation = false;
    private float frameTimer;
    private int currentFrame;
    private double lastTime;

    public override void OnBodyGUI()
    {
        base.OnBodyGUI();
        if (GUILayout.Button("Toggle Animation"))
        {
            showAnimation = !showAnimation;
            // Repaint 강제 호출하면 즉시 반영됨
            NodeEditorWindow.current.Repaint();
        }

        if (!showAnimation)
        {
            // 애니메이션을 그리지 않고 종료
            return;
        }
        var node = target as blowing;

        if (node.sprites == null || node.sprites.Length == 0)
            return;

        // 시간 계산
        double time = EditorApplication.timeSinceStartup;
        if (time - lastTime > 0.1f) // 0.1초마다 다음 프레임
        {
            currentFrame = (currentFrame + 1) % node.sprites.Length;
            lastTime = time;
        }
        Rect rect = GUILayoutUtility.GetRect(150, 150);
        Texture2D texture = AssetPreview.GetAssetPreview(node.sprites[currentFrame]);
        if (texture != null)
        {
            texture.filterMode = FilterMode.Point;

            
            GUI.DrawTexture(rect, texture, ScaleMode.ScaleToFit);
            //GUILayout.Label(texture, GUILayout.Width(100), GUILayout.Height(100));
        }
    }
}
[CustomNodeEditor(typeof(purgeCannon))]
public class purgeCannonPlay : NodeEditor
{
    bool showAnimation = false;
    private float frameTimer;
    private int currentFrame;
    private double lastTime;

    public override void OnBodyGUI()
    {
        base.OnBodyGUI();
        if (GUILayout.Button("Toggle Animation"))
        {
            showAnimation = !showAnimation;
            // Repaint 강제 호출하면 즉시 반영됨
            NodeEditorWindow.current.Repaint();
        }

        if (!showAnimation)
        {
            // 애니메이션을 그리지 않고 종료
            return;
        }
        var node = target as purgeCannon;

        if (node.sprites == null || node.sprites.Length == 0)
            return;

        // 시간 계산
        double time = EditorApplication.timeSinceStartup;
        if (time - lastTime > 0.1f) // 0.1초마다 다음 프레임
        {
            currentFrame = (currentFrame + 1) % node.sprites.Length;
            lastTime = time;
        }
        Rect rect = GUILayoutUtility.GetRect(150, 150);
        Texture2D texture = AssetPreview.GetAssetPreview(node.sprites[currentFrame]);
        if (texture != null)
        {
            texture.filterMode = FilterMode.Point;

           
            GUI.DrawTexture(rect, texture, ScaleMode.ScaleToFit);
            //GUILayout.Label(texture, GUILayout.Width(100), GUILayout.Height(100));
        }
    }
}
[CustomNodeEditor(typeof(purgeShot))]
public class purgeShotPlay : NodeEditor
{
    bool showAnimation = false;
    private float frameTimer;
    private int currentFrame;
    private double lastTime;

    public override void OnBodyGUI()
    {
        base.OnBodyGUI();
        if (GUILayout.Button("Toggle Animation"))
        {
            showAnimation = !showAnimation;
            // Repaint 강제 호출하면 즉시 반영됨
            NodeEditorWindow.current.Repaint();
        }

        if (!showAnimation)
        {
            // 애니메이션을 그리지 않고 종료
            return;
        }
        var node = target as purgeShot;

        if (node.sprites == null || node.sprites.Length == 0)
            return;

        // 시간 계산
        double time = EditorApplication.timeSinceStartup;
        if (time - lastTime > 0.1f) // 0.1초마다 다음 프레임
        {
            currentFrame = (currentFrame + 1) % node.sprites.Length;
            lastTime = time;
        }
        Rect rect = GUILayoutUtility.GetRect(150, 150);
        Texture2D texture = AssetPreview.GetAssetPreview(node.sprites[currentFrame]);
        if (texture != null)
        {
            texture.filterMode = FilterMode.Point;

         
            GUI.DrawTexture(rect, texture, ScaleMode.ScaleToFit);
            //GUILayout.Label(texture, GUILayout.Width(100), GUILayout.Height(100));
        }
    }
}
 