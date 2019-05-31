Sanford.Multimedia.Midi
=======================

C# 课程实验2

![winform](https://raw.githubusercontent.com/8qwe24657913/Sanford.Multimedia.Midi-master/master/Images/winform.gif)

## 功能概述

除简单播放外，还实现了：

1. 单曲循环、列表循环功能开关
2. 播放列表长度大于1时显示播放列表，可点击播放列表切歌
3. 简化了界面和操作逻辑，使之比起代码逻辑本身更符合人类的操作习惯
4. 采用双层窗口技巧使界面半透明，更具美感
5. 计算总时长和已播放时长，并实时更新标题，方便用户直观观察播放时间
6. 解决了原程序出现死锁问题的bug
7. 可直接拖拽文件到窗口内来播放

## 实现细节

1. 关于循环播放：

不能在播放结束事件中直接调用播放开始函数，因为播放结束函数并不会在主线程运行，调用时会报错

如果使用 Invoke() 在主线程调用的话依然会报错，因为此事件产生时播放器并未做好下一次播放的准备，一个迭代器未被重置，二次遍历报错

![img](https://raw.githubusercontent.com/8qwe24657913/Sanford.Multimedia.Midi-master/master/Images/invoke.png) 

如果使用反射强行重置该迭代器，会观察到死锁现象，因为MIDI 播放器为了保证操作的原子性，在内部使用了锁，而对 MIDI 播放器的调用只能在主线程，需要 Invoke() 方法，Invoke() 方法会打断目标线程正在执行的代码，直到操作完成，如果 Invoke() 时恰好 MIDI 的锁被占用，Action 获取不到锁，就会等待锁的释放，而锁的释放又要等待 Invoke() 完成，于是造成了程序的假死现象

![img](https://raw.githubusercontent.com/8qwe24657913/Sanford.Multimedia.Midi-master/master/Images/lock.png) 

2. 关于时间显示

项目自带的“进度条”并不能标识播放时间，因为它标识的是 Ticks，而Ticks 与时间的关系中要乘上拍速，拍速又可以由 meta message 动态改变，导致时间与用户直观看到的进度条不符

![img](https://raw.githubusercontent.com/8qwe24657913/Sanford.Multimedia.Midi-master/master/Images/tempo.png) 

为了计算时间，我们需要遍历整个 MIDI 文件的 meta message，取拍速 tempo，并分别与 Ticks 相乘，再加和得到时间

默认的拍速应为 400000,（与 Windows Media Player 一致），而该项目中默认拍速为 500000，这就需要修改 clock.Tempo，而clock 不是 public 属性，需要用反射机制做一些小小的黑魔法：

![img](https://raw.githubusercontent.com/8qwe24657913/Sanford.Multimedia.Midi-master/master/Images/blackmagic.png) 

3. 关于透明的实现

WindowsForm 对于透明的支持极差，唯二的透明方法是设定 Opacity和 TransparencyKey，前者会导致整个窗口（包括文字和控件）都变得透明，难以直接使用，后者的工作原理是先画好整个窗口，再将颜色与 TransparencyKey 相同的像素直接扣掉，问题在于被扣掉的位置完全没有颜色，而且会有鼠标穿透，再加上文字的边缘像素颜色既与文字颜色不同，又与 TransparencyKey 不同，会留下难看的白边

我这里采用的是一种叫做双层窗口的 Hack 方法，创建两个窗口，前面的窗口显示文字和控件，并设置 TransparencyKey 属性扣掉背景像素，后面的窗口设定 Opacity 属性，提供一个半透明背景并淡化文字边缘像素的影响，并帮助接收拖拽文件进入窗口等事件解决鼠标穿透问题

![img](https://raw.githubusercontent.com/8qwe24657913/Sanford.Multimedia.Midi-master/master/Images/transparent.png) 

由于 WindowsForm 本身的限制，在背景颜色很深时文字仍会有白边，造成不好的用户体验，要解决这个问题就只能抛弃 WindowsForm 的透明机制，直接使用 Windows API 或者自绘窗口，网络上有收费的透明界面库，但考虑到该项目的复杂度等问题并没有使用

![img](https://raw.githubusercontent.com/8qwe24657913/Sanford.Multimedia.Midi-master/master/Images/whiteedge.png)

## 项目特色

☑ 功能丰富

☑ 易于操作

☑ 美观大方

☑ 实用性强

## 代码总量

约 550 行左右

## 工作时间

约三~五晚

## 结论

1. MIDI 文件本身没有时长字段，原程序按tick数而非时间设置进度条，会误导使用者。通过遍历拍速meta信息计算拍速对应的每tick时长与tick数的乘积之和才能得到真正时长

2. `WinForm` 技术栈较为过时，不能很好的支持透明，双层窗口的 trick 充其量是一种 hack，会造成文字周围白边，真正需要透明的话还是要用 `WPF` 或者直接调 `Windows API`