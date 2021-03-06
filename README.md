# 方块陷落

2020 SUSTech Computer Graphics Project

## 游戏指南

**游戏目标**

在最后一个方块陷落前存活，你需要通过踩死怪物从而让所有方块陷落！

**玩家控制**

前后左右：WSAD

跳跃：Space

视角变换：鼠标移动

视角缩放：鼠标滚轮

**编辑模式控制**

前后左右：WSAD

向上飞：Space

向下飞：Ctrl + Space

切换编辑模式：Q/E

CubeMode下：滚轮切换方块种类，鼠标左键摧毁方块，鼠标右键放置方块

PlayerMode下：右键放置玩家出生位置

MonsterMode下：右键放置怪物，左键摧毁怪物，滚轮切换怪物种类(目前只有一种怪物)



**游戏说明**

每个怪物的死亡都会产生一个死亡扰动，触发下方的方块发生陷落。有些方块的陷落会传递扰动导致连锁陷落。不同的方块具有不同的属性，会产生不同的效果...

你可以在Story->TUTORIAL 关卡下测试这些方块都有什么效果。如果你准备好了，可以试玩Story下的两张挑战地图，或是在Edit下编辑你自己的关卡。



## 贡献

本项目处于初级学习阶段，欢迎所有人交流想法或为此项目提供issue。



## 游戏设计

**TODO List**

**完善岩石和沙土的功能**

- [x] 坠落连锁
- [x] 坠落一定时间后消失
- [x] 坠落调试接口

**编辑界面存档**

- [x] 自定义存档名称

**人物操控**

- [x] 摄像机的缩放
- [ ] 死亡动作
- [ ] 优化操控细节

**更多方块**

- [ ] 云
- [ ] 冰块
- [ ] 沼泽

**环境贴图**

- [ ] 底部云层
- [ ] 天空盒

**游戏Nevigation**

- [x] PLAY
  - [x] LOAD MAP
    - [x] BACK
    - [x] START
      - [x] ESC
        - [x] Replay
        - [x] Menu
        - [x] Save
        - [x] Quit
  - [ ] STORY
  - [x] EDIT
  - [x] BACK
- [x] OPTION
  - [ ] VOLUME
  - [ ] CONTROL SETTING

- [x] QUIT

**怪物系统**

- [x] 模型
- [x] 行走系统
- [ ] 死亡事件
- [ ] 仇恨系统

**第一关设计**



**BUG TO FIX**



## 调试

**热键说明**

`F3` Debug窗口显示与隐藏

`F5` 游戏场景切换成人物Debug模式/正常模式，在Debug模式下，按E键触发人物所在位置的方块坠落事件(替代敌人死亡)



**地图编辑模式说明**

在地图编辑模式下，目前有两个编辑状态

1. 方块编辑模式(默认)
2. 玩家位置编辑模式(按P键切换)

破坏与建造：

在方块编辑模式下左键破坏，右键放置，滚轮切换方块种类

在玩家编辑模式下右键放置玩家出生位置

位移：

WSAD 前后左右移动，空格向上移动，Shift向下移动



**游戏模式操作说明**

游戏模式下有两种状态

1. 正常游玩状态
2. Debug状态

在正常游玩状态下人物操作：

WSAD行走，空格跳跃，鼠标移动切换视角，滚轮拉伸视角

Debug状态(按F5 切换)

WSAD前后左右移动，空格向上飞，shift向下飞，E键触发死亡事件，R键恢复





## **方块陷落：游戏机制**

关卡驱动的单人/本地多人/线上多人 益智游戏。

第三人称



## 主角能力

**位移**

前后左右位移，会被地形阻挡，碰到怪物死亡

**跳跃**

向上位移，落下时具有杀死脚下怪物的能力

**交互道具**

使用道具功能，消耗一次交互次数

**丢弃道具**

放弃当前道具

**溶入（可选）**

溶入某个物质方块，可以躲避怪物视线或阻止连锁陷落。

一定冷却时间后才可再次使用

**吞食/吐出（可选）**

使一个物质方块消失（暂存在体内），并拥有对应的效果。

体内最多具有一个物质方块，再次吞食需要吐出体内的方块

一定冷却时间后才可再次吞食

**攻击（多人）（可选）**

使攻击范围内一个方块单独陷落。

**加速**

使角色在一定时间内提速



## 方块种类

### **属性**

- 破坏次数（1）
- 可连锁
- 摩擦系数
- 下落速度
- 可站立时间
- 可吞噬
  - 重量
- 可融合

**岩石**

方块世界最常见的物质，坚硬，重量大

- 破坏次数	(1）
- 可连锁 	(False)
- 摩擦系数	(1)
- 最大速度	(1)
- 下落速度	(1)
- 可站立时间(无限)
- 可吞噬	(True)
  - 重量	 (2)
- 可融合	(False)



**砂土**

1. 方块世界最常见的物质，生物在上方死亡时会造成陷落，有连带效应
2. 玩家可吞食（吞食后产生少量负重）
3. 可融合

- 破坏次数		(1）
- 可连锁		(True）
- 摩擦系数		(1)
- 最大速度		(1)
- 下落速度		(1)
- 可站立时间	(无限）
- 可吞噬		(True)
  - 重量		(1)
- 可融合		(True)





**云**

1. 轻盈的物质，站在上方超过一段时间将触发陷落
2. 玩家可吞食（吞食后跳跃高度增加）

可融合



- 破坏次数	（1）
- 可连锁	（False）
- 摩擦系数	（1.0）
- 最大速度	  (1)
- 下落速度	（1）
- 可站立时间（2s）
- 可吞噬	（True）
  - 重量	（0）
- 可融合	（True）





**冰块**

1. 光滑而又冰冷的物质，站在上面容易打滑
2. 玩家可吞噬（速度增加）

- 破坏次数	（1）
- 可连锁	（False）
- 摩擦系数	(0.5)
- 最大速度	（1.5）
- 下落速度	(1)
- 可站立时间(无限)
- 可吞噬	（True）
  - 重量	（1）
- 可融合	（True）



**沼泽**

1. 站在上面容易静止
2. 玩家可吞噬，吞噬cd边长
3. 可融合

- 破坏次数		(1)
- 可连锁		(False)
- 摩擦系数		(1.5)
- 最大速度		(0.5)
- 下落速度		(1)
- 可站立时间	(无限)
- 可吞噬		(True)
  - 重量	（1）
- 可融合		(True)



### **道具**

只有玩家可携带，实时交互的物品（同一时间，只能持有一个）

- 重力（运动方式）
- 速度（运动方式）
- 交互功能
- 交互次数





**投掷物（手榴弹）**

1. ​	大面积破坏地（陷落/消除？）
2. 触发有延时



**枪**

1. ​	破坏地形

#### **场景**

地图中可直接交互设施（怪物也可）（类buff）

依附于方块（下落速度等于依附的方块）

- 使用次数（无穷）
- 持续时间
- buff效果



**弹跳床**

1. 玩家在跳跃时增加跳跃高度

- 使用次数（无穷）
- 持续时间（0.1）
- buff效果（强制1.5倍跳跃）





**加速果实/符文**

1. ​	使用次数为1，增加速度

- 使用次数（1）
- 持续时间（10s）
- buff效果（1.5倍速度）





**加速带（可选）**

- 使用次数（无穷）
- 持续时间（）

## 敌人

### 属性

*  是否可用（isPaused）
*  是否移动、旋转（isMoving, isRotating）
*  当前朝向
*  移动速度
*  生命
*  仇恨范围
*  行动逻辑（method）
*  死亡事件（method）
*  追踪事件（method）

### Breaker

*  HP 1
*  speed 0.2
*  traceDepth 5
最普通的小怪。死亡时对下方方块进行塌陷。
