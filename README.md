# NovaBrush_2-brushsys Module
Brush, Pencil, Eraser, AA toggle, size slider, and image saving!

This is Module 2 of **NovaBrush**, a lightweight, responsive pixel editor built with **C#**.
NovaBrush manages to load images at **under 100MB on 4K images**.

<img src="https://github.com/eduardodias2002/eduardodias2002.github.io/blob/main/img/scrnsh1.webp">

## What It Does

**Smart Brush Engine**  
- Pencil (1px, crisp, pixel-perfect)  
- Brush (soft, anti-aliased, builds up)  
- Eraser (pressure-sensitive, fades gradually)  

**Anti-Aliasing Toggle**  
- Switch between smooth (blended) and hard (crisp) strokes  
- Made for painting and pixel art 

**Brush Size Control**  
- Real-time slider adjusts size from 1-50px

**New Image System**  
- Converts all loaded images to `Bgra32` for full alpha support, transparency wasn't supported previously
- Enables erasing on imported images, and prevents memory leaks and format mismatches  

**Save Images**  
- Only to PNG as of now
- Preserves transparency (PNG)  

**Tool Window**  
- Toggle tools on/off  
- Visual feedback for active tool  

**ITool Interface**  
- Modular brush system  
- Easy to extend
- One handler for all future tools

**Globals Class**  
- Central nervous system for shared state  
- Safe access to bitmap, tools, and settings  
- Designed for scalability  

**Efficient Pixel Drawing**  
- Bounds-checked, memory-safe  
- Smooth line interpolation (Bresenham) as opposed to clicking once in the position per pixel
- Issue with pixel gaps solved

---

## Modules...
This is **Module 2** of NovaBrush:
1. [Module 1](https://github.com/eduardodias2002/NovaBrush_1-pixel-array/) - Canvas, Load, Pan, Pixels  
2. [Module 2](https://github.com/eduardodias2002/NovaBrush_2-brushsys) - Brush, Eraser, AA, Save, Tools  

Devlog soon! [NovaBrush on my site](eduardodias2002.github.io)
