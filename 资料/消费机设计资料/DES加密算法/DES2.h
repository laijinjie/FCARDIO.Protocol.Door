/*----------------------------------------------------------------
// Copyright (C) 2008 尹学渊
// 版权所有。
//
// 文件名：yxyDES2.h
// 文件功能描述：DES2加密模块 c语言版
//
//
// 创建人：尹学渊
//
// 修改人：
// 修改描述：
//
// 修改人：
// 修改描述：
//----------------------------------------------------------------*/



char szSubKeys[16][48];//储存16组48位密钥
char szCiphertextRaw[64]; //储存二进制密文(64个Bits) int 0,1
char szPlaintextRaw[64]; //储存二进制密文(64个Bits) int 0,1
char szCiphertextInBytes[8];//储存8位密文
char szPlaintextInBytes[8];//储存8位明文字符串

char szCiphertextInBinary[65]; //储存二进制密文(64个Bits) char '0','1',最后一位存'\0'



//初始化函数
void DES_Initialize(); 

//功能:产生16个28位的key
//参数:源8位的字符串(key)
//结果:函数将调用private CreateSubKey将结果存于char SubKeys[16][48]
void DES_InitializeKey(char* srcBytes);

//功能:加密8位字符串
//参数:8位字符串
//结果:函数将加密后结果存放于private szCiphertext[16]
//      用户通过属性Ciphertext得到
void DES_EncryptData(char* srcBytes);

//功能:解密16位十六进制字符串
//参数:16位十六进制字符串
//结果:函数将解密候结果存放于private szPlaintext[8]
//      用户通过属性Plaintext得到
void DES_DecryptData(char* srcBytes);

//功能:加密任意长度字符串
//参数:任意长度字符串,长度
//结果:函数将加密后结果存放于private szFCiphertextAnyLength[8192]
//      用户通过属性CiphertextAnyLength得到
void DES_EncryptAnyLength(char* _srcBytes,unsigned int _bytesLength);

//功能:解密任意长度十六进制字符串
//参数:任意长度字符串,长度
//结果:函数将加密后结果存放于private szFPlaintextAnyLength[8192]
//      用户通过属性PlaintextAnyLength得到
void DES_DecryptAnyLength(char* _srcBytes,unsigned int _bytesLength);

//功能:Bytes到Bits的转换,
//参数:待变换字符串,处理后结果存放缓冲区指针,Bits缓冲区大小
void DES_Bytes2Bits(char *srcBytes, char* dstBits, unsigned int sizeBits);

//功能:Bits到Bytes的转换,
//参数:待变换字符串,处理后结果存放缓冲区指针,Bits缓冲区大小
void DES_Bits2Bytes(char *dstBytes, char* srcBits, unsigned int sizeBits);

//功能:Int到Bits的转换,
//参数:待变换字符串,处理后结果存放缓冲区指针
void DES_Int2Bits(unsigned int srcByte, char* dstBits);
		
//功能:Bits到Hex的转换
//参数:待变换字符串,处理后结果存放缓冲区指针,Bits缓冲区大小
void DES_Bits2Hex(char *dstHex, char* srcBits, unsigned int sizeBits);
		
//功能:Bits到Hex的转换
//参数:待变换字符串,处理后结果存放缓冲区指针,Bits缓冲区大小
void DES_Hex2Bits(char *srcHex, char* dstBits, unsigned int sizeBits);


//Ciphertext的get函数
char* DES_GetCiphertextInBytes();

//Plaintext的get函数
char* DES_GetPlaintext();

//获取加密后的数据及长度
char* DES_GetCiphertextAnyLength();
int DES_GetCiphertextAnyLengthSize();

//获取解密后的数据及长度
char* DES_GetPlaintextAnyLength();
int DES_GetPlaintextAnyLengthSize();

//功能:生成子密钥
//参数:经过PC1变换的56位二进制字符串
//结果:将保存于char szSubKeys[16][48]
void DES_CreateSubKey(char* sz_56key);

//功能:DES中的F函数,
//参数:左32位,右32位,key序号(0-15)
//结果:均在变换左右32位
void DES_FunctionF(char* sz_Li,char* sz_Ri,unsigned int iKey);

//功能:IP变换
//参数:待处理字符串,处理后结果存放指针
//结果:函数改变第二个参数的内容
void DES_InitialPermuteData(char* _src,char* _dst);

//功能:将右32位进行扩展位48位,
//参数:原32位字符串,扩展后结果存放指针
//结果:函数改变第二个参数的内容
void DES_ExpansionR(char* _src,char* _dst);

//功能:异或函数,
//参数:待异或的操作字符串1,字符串2,操作数长度,处理后结果存放指针
//结果: 函数改变第四个参数的内容
void DES_XOR(char* szParam1,char* szParam2, unsigned int uiParamLength, char* szReturnValueBuffer);

//功能:S-BOX , 数据压缩,
//参数:48位二进制字符串,
//结果:返回结果:32位字符串
void DES_CompressFuncS(char* _src48, char* _dst32);

//功能:IP逆变换,
//参数:待变换字符串,处理后结果存放指针
//结果:函数改变第二个参数的内容
void DES_PermutationP(char* _src,char* _dst);

 