---
title: winform-control-event
---

A Windows Forms control inherits more than sixty events from System.Windows.Forms.Control. These include the Paint event, which causes a control to be drawn, events related to displaying a window, such as the Resize and Layout events, and low-level mouse and keyboard events. Some low-level events are synthesized by Control into semantic events such as Click and DoubleClick. For details about inherited events, see Control

If your custom control needs to override inherited event functionality, override the inherited OnEventName method instead of attaching a delegate

