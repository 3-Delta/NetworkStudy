using System;

// Path: Assets/Scripts/Base/FSMByName/BS_IDo.cs.
// SvnVersion: -1.
// Author: kaclok created 2019/01/11 00:17:06 Friday on pc: KACLOK.
// Copyright@nullgame`s testgame. All rights reserved.

// https://www.jianshu.com/p/167f0f59b505
public interface IDo<T>
{
    bool PreDo();
    T Do();
    void PostDo();
}