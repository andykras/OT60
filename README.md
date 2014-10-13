#Original Tetris from Elektronika 60#

This is my clone of original tetris from Elektronika 60 written on C#. The purpose of this exercise is to learn design patterns. It's absolutely free to share and non-commercial.

I've tried to implement game engine for general case. It manipulates abstract game objects and didn't know anything about how to render the same. I've made default implementation for console graphics and for tetris blocks as I remember from my DVK2 machine. 

##Here is an example:##
```
ПОЛНЫХ СТРОК:  3        <! . . . . . . . . . .!>
УРОВЕНЬ:       7        <! . . . . . . . . . .!>   7: НАЛЕВО   9: НАПРАВО
  СЧЕТ:  417            <! . . . . . . . . . .!>        8:ПОВОРОТ
                        <! . . . . . . . . . .!>   4:УСКОРИТЬ  5:СБРОСИТЬ
                        <! . . . . . . . . . .!>   1: ПОКАЗАТЬ  СЛЕДУЮЩУЮ
                        <! . . . . . . . . . .!>   0:  СТЕРЕТЬ ЭТОТ ТЕКСТ
                        <! . . . . . . . . . .!>     ПРОБЕЛ - СБРОСИТЬ
                        <! . . . . . . . . . .!>
                        <! . . . . . . . . . .!>
                        <! . . . . . . . . . .!>
               [][][][] <! . . . . . . . . . .!>
                        <! . . . . . . . . . .!>
                        <! . . . . . . . . . .!>
                        <! .[][] . . . . . . .!>
                        <! . .[] . . . . . . .!>
                        <! . .[] . . . . . . .!>
                        <! . . . . . . . . . .!>
                        <! . . . . . . . . . .!>
                        <! . . .[] . . . . . .!>
                        <! . .[][][] . . . . .!>
                        <!====================!>
                          \/\/\/\/\/\/\/\/\/\/
```

You can easily implement any stuff you want - different console blocks, different graphics system, different numbers of squares - for example Pentomino, and others.

**Code is a poetry**

[andykras]:http://andykras.org
