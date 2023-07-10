union U {
    uint16_t value; // 整体对外协议号
    struct ST {
        uint16_t gm : 1; // 是否来自gm
        uint16_t first : 8; // module编号
        uint16_t second : 7; // 模块内协议号
    }st;
};
