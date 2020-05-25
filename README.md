# Cube-Falling



**调试热键说明**

关于全局的Input输入在`GameManager`中定义

`F1` 保存地图(编辑模式下)，当前只有默认保存

保存名称：`auto_save[DateTime]`

保存路径：`Application.persistentDataPath/` (输出后应该在游戏的Data目录下，编辑器模式保存在Unity系统设置的永久存储目录)



`F2` 加载地图(编辑模式下)，没有做地图选择，加载的地图在`GameManager`中硬编码

```c#
if (Input.GetKeyDown(KeyCode.F2))
            world.LoadWorld("auto_save2020-05-24-23-46.data");
```

根据调试需要更改地图名称

之后尽快添加目录选择功能



`F3` Debug窗口显示与隐藏

`F4` 切换编辑/游戏场景





**地图编辑模式说明**

在地图编辑模式下，目前有两个编辑状态

1. 方块编辑模式(默认)
2. 玩家位置编辑模式(按P键切换)

在方块编辑模式下左键破坏，右键放置，滚轮切换方块种类

在玩家编辑模式下右键放置玩家出生位置