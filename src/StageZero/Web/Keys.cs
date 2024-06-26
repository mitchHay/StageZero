using System;

namespace StageZero.Web;

[Flags]
public enum Keys
{
    // FUNCTIONS
    Control = 0x1,
    Shift = 0x2,
    Tab = 0x3,
    Alt = 0x4,
    Delete = 0x5,
    UpArrow = 0x6,
    RightArrow = 0x7,
    DownArrow = 0x8,
    LeftArrow = 0x9,
    F1 = 0x10,
    F2 = 0x11,
    F3 = 0x12,
    F4 = 0x13,
    F5 = 0x14,
    F6 = 0x15,
    F7 = 0x16,
    F8 = 0x17,
    F9 = 0x19,
    F10 = 0x20,
    F11 = 0x21,
    F12 = 0x22,
   
    // QUERTY
    A = 0x100,
    B = 0x101,
    C = 0x102,
    D = 0x103,
    E = 0x104,
    F = 0x105,
    G = 0x106,
    H = 0x107,
    I = 0x108,
    J = 0x109,
    K = 0x110,
    L = 0x111,
    M = 0x112,
    N = 0x113,
    O = 0x114,
    P = 0x115,
    Q = 0x116,
    R = 0x117,
    S = 0x118,
    T = 0x119,
    U = 0x120,
    V = 0x121,
    W = 0x122,
    X = 0x123,
    Y = 0x124,
    Z = 0x125,

    // NUMBERS
    Zero = 0x200,
    One = 0x201,
    Two = 0x202,
    Three = 0x203,
    Four = 0x204,
    Five = 0x205,
    Six = 0x206,
    Seven = 0x207,
    Eight = 0x208,
    Nine = 0x209
}