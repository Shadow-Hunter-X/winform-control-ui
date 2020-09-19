---
title: Forms Properties
---

A Windows Forms control inherits many properties form the base class System.Windows.Forms.Control. These include properties such as Font, ForeColor, BackColor, Bounds, ClientRectangle, DisplayRectangle, Enabled, Focused, Height, Width, Visible, AutoSize, and many others. For details about inherited properties, see System.Windows.Forms.Control.

You can override inherited properties in your control as well as define new properties.

Windows 窗体控件从基类系统继承许多属性。这些属性包括属性，如字体，前色，背面颜色，边界，客户端矩形，显示矩形，启用，聚焦，高度，宽度，可见，自动大小，和许多其他。有关继承属性的详细信息，请参阅System.Windows.forms.Control。可以重写控件中的继承属性以及定义新属性。

控件中的property和attribute间的关系： property像是OO思想中具体的对象属性，而attribute这是property的具体的形式。

### 定义属性

关于属性：
	A component should define properties instead of public fields, because visual designers such as Visual Studio display properties, but not fields, in the property browser. (Other compelling reasons to define properties are listed at the end of this topic)
	
在C#中属性和字段的差异认识。

* 字段：与类相关的变量
声明方法与声明变量类似，可在前面添加访问修饰符、static关键字等。

*属性：与字段类似，结尾不加分号，有一对大括号，其中包含get、set访问器的相关代码。
即含有get、set即为属性，使用属性的目的是在get、set中添加数据访问限定代码，鼓励使用属性替代常规字段

defines a custom property named EndColor for the custom control FlashTrackBar
~~~C#
public class FlashTrackBar : Control {  
   ...  
   // Private data member that backs the EndColor property.  
   private Color endColor = Color.LimeGreen;  
   // The Category attribute tells the designer to display  
   // it in the Flash grouping.
   // The Description attribute provides a description of  
   // the property.
   [  
   Category("Flash"),  
   Description("The ending color of the bar.")  
   ]  
   // The public property EndColor accesses endColor.  
   public Color EndColor {  
      get {  
         return endColor;  
      }  
      set {  
         endColor = value;  
         if (baseBackground != null && showGradient) {  
            baseBackground.Dispose();  
            baseBackground = null;  
         }  
         // The Invalidate method calls the OnPaint method, which redraws
         // the control.  
         Invalidate();  
      }  
   }  
   ...  
}
~~~

### 定义属性默认值

ShouldSerialize and Reset are optional methods that you can provide for a property, if the property does not a have simple default value. If the property has a simple default value, you should apply the DefaultValueAttribute and supply the default value to the attribute class constructor instead. Either of these mechanisms enables the following features in the designer

可使用ShouldSerialize和Reset方法未为设置一个默认值的控件，进行设置。如果属性具有简单的默认值，则应应用DefaultValueAttribute，然后将默认值提供到属性类构造函数。
应用的场景。

* 如果属性已从默认值修改，则属性在属性浏览器中提供可视指示
* 用户可以右键单击该属性，然后选择"重置"以将属性还原到其默认值
* 设计器生成更高效的代码

When declaring a ShouldSerialize or Reset method, use the private access modifier. These methods are usually invoked by the designer and not by user code

If a property does not have a Reset method, is not marked with a DefaultValueAttribute, and does not have a default value supplied in its declaration, the Reset option for that property is disabled in the shortcut menu of the Properties window of the Windows Forms Designer in Visual Studio
如果属性没有方法，未使用DefaultValueAttribute标记，并且其声明中没有提供默认值，则该属性的选项将禁用在 Visual Studio 中 Windows 窗体设计器"属性"窗口窗口的快捷菜单中， 属性ResetMyFont()。

~~~C#
// The ResetPropertyName method sets a property to its default value, as shown in the following code fragment.
private void ResetMyFont()
{
   MyFont = null;
}
~~~

Designers such as Visual Studio use the ShouldSerializePropertyName method to check whether a property has changed from its default value and write code into the form only if a property is changed, thus allowing for more efficient code generation

~~~C#
// Returns true if the font has changed; otherwise, returns false.
// The designer writes code to the form only if true is returned.
private bool ShouldSerializeMyFont()
{
   return thefont != null;
}
~~~

完整的实例代码 

~~~C#
using System;
using System.Drawing;
using System.Windows.Forms;

public class MyControl : Control {
   // Declare an instance of the Font class
   // and set its default value to null.
   private Font thefont = null;

   // The MyFont property.
   public Font MyFont {
      // Note that the MyFont property never
      // returns null.
      get {
         if (thefont != null) return thefont;
         if (Parent != null) return Parent.Font;
         return Control.DefaultFont;
      }
      set {
         thefont = value;
      }
   }

   private bool ShouldSerializeMyFont()
   {
      return thefont != null;
   }

   private void ResetMyFont()
   {
      MyFont = null;
   }
}
~~~

### 属性的事件

If you want your control to send notifications when a property named PropertyName changes, define an event named PropertyNameChanged and a method named OnPropertyNameChanged that raises the event. The naming convention in Windows Forms is to append the word Changed to the name of the property. The associated event delegate type for property-changed events is EventHandler, and the event data type is EventArgs. The base class Control defines many property-changed events, such as BackColorChanged, BackgroundImageChanged, FontChanged, LocationChanged, and others. For background information about events, see Events and Events in Windows Forms Controls.

Property-changed events are useful because they allow consumers of a control to attach event handlers that respond to the change. If your control needs to respond to a property-changed event that it raises, override the corresponding OnPropertyNameChanged method instead of attaching a delegate to the event. A control typically responds to a property-changed event by updating other properties or by redrawing some or all of its drawing surface.

The following example shows how the FlashTrackBar custom control responds to some of the property-changed events that it inherits from Control. For the complete sample.下面的示例演示自定义控件如何响应从 Control 继承的一些属性更改事件
~~~C#
protected override void OnTextChanged(EventArgs e) {
    base.OnTextChanged(e);
    Invalidate();
}

protected override void OnBackColorChanged(EventArgs e) {
    base.OnBackColorChanged(e);
    if ((baseBackground != null) && (!showGradient)) {
                baseBackground.Dispose();
                baseBackground = null;
    }
}
~~~

### Expose Properties of Constituent Controls -- 公开控件属性

The controls that make up a composite control are called constituent controls. These controls are normally declared private, and thus cannot be accessed by the developer. If you want to make properties of these controls available to future users, you must expose them to the user. A property of a constituent control is exposed by creating a property in the user control, and using the get and set accessors of that property to effect the change in the private property of the constituent control.

The following example shows how to expose the BackColor property of the constituent button:
~~~C#
public Color ButtonColor
{
   get
   {
      return(myButton.BackColor);
   }
   set
   {
      myButton.BackColor = value;
   }
}
~~~



