using System;

namespace StageZero.Web;

[Flags]
public enum Keys
{
    // FUNCTIONS
    Control = 0,
    Shift = 1 << 0,
    Tab = 1 << 1,
    Alt = 1 << 2,
    Delete = 1 << 3,
    UpArrow = 1 << 4,
    RightArrow = 1 << 5,
    DownArrow = 1 << 6,
    LeftArrow = 1 << 7,
    F1 = 1 << 8,
    F2 = 1 << 9,
    F3 = 1 << 10,
    F4 = 1 << 11,
    F5 = 1 << 12,
    F6 = 1 << 13,
    F7 = 1 << 14,
    F8 = 1 << 15,
    F9 = 1 << 16,
    F10 = 1 << 17,
    F11 = 1 << 18,
    F12 = 1 << 19,
   
    // QUERTY
    A = 1 << 100,
    B = 1 << 101,
    C = 1 << 102,
    D = 1 << 103,
    E = 1 << 104,
    F = 1 << 105,
    G = 1 << 106,
    H = 1 << 107,
    I = 1 << 108,
    J = 1 << 109,
    K = 1 << 110,
    L = 1 << 111,
    M = 1 << 112,
    N = 1 << 113,
    O = 1 << 114,
    P = 1 << 115,
    Q = 1 << 116,
    R = 1 << 117,
    S = 1 << 118,
    T = 1 << 119,
    U = 1 << 120,
    V = 1 << 121,
    W = 1 << 122,
    X = 1 << 123,
    Y = 1 << 124,
    Z = 1 << 125,

    // NUMBERS
    Zero = 1 << 200,
    One = 1 << 201,
    Two = 1 << 202,
    Three = 1 << 203,
    Four = 1 << 204,
    Five = 1 << 205,
    Six = 1 << 206,
    Seven = 1 << 207,
    Eight = 1 << 208,
    Nine = 1 << 209
}