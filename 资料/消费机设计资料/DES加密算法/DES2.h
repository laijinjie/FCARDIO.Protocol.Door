/*----------------------------------------------------------------
// Copyright (C) 2008 ��ѧԨ
// ��Ȩ���С�
//
// �ļ�����yxyDES2.h
// �ļ�����������DES2����ģ�� c���԰�
//
//
// �����ˣ���ѧԨ
//
// �޸��ˣ�
// �޸�������
//
// �޸��ˣ�
// �޸�������
//----------------------------------------------------------------*/



char szSubKeys[16][48];//����16��48λ��Կ
char szCiphertextRaw[64]; //�������������(64��Bits) int 0,1
char szPlaintextRaw[64]; //�������������(64��Bits) int 0,1
char szCiphertextInBytes[8];//����8λ����
char szPlaintextInBytes[8];//����8λ�����ַ���

char szCiphertextInBinary[65]; //�������������(64��Bits) char '0','1',���һλ��'\0'



//��ʼ������
void DES_Initialize(); 

//����:����16��28λ��key
//����:Դ8λ���ַ���(key)
//���:����������private CreateSubKey���������char SubKeys[16][48]
void DES_InitializeKey(char* srcBytes);

//����:����8λ�ַ���
//����:8λ�ַ���
//���:���������ܺ��������private szCiphertext[16]
//      �û�ͨ������Ciphertext�õ�
void DES_EncryptData(char* srcBytes);

//����:����16λʮ�������ַ���
//����:16λʮ�������ַ���
//���:���������ܺ��������private szPlaintext[8]
//      �û�ͨ������Plaintext�õ�
void DES_DecryptData(char* srcBytes);

//����:�������ⳤ���ַ���
//����:���ⳤ���ַ���,����
//���:���������ܺ��������private szFCiphertextAnyLength[8192]
//      �û�ͨ������CiphertextAnyLength�õ�
void DES_EncryptAnyLength(char* _srcBytes,unsigned int _bytesLength);

//����:�������ⳤ��ʮ�������ַ���
//����:���ⳤ���ַ���,����
//���:���������ܺ��������private szFPlaintextAnyLength[8192]
//      �û�ͨ������PlaintextAnyLength�õ�
void DES_DecryptAnyLength(char* _srcBytes,unsigned int _bytesLength);

//����:Bytes��Bits��ת��,
//����:���任�ַ���,���������Ż�����ָ��,Bits��������С
void DES_Bytes2Bits(char *srcBytes, char* dstBits, unsigned int sizeBits);

//����:Bits��Bytes��ת��,
//����:���任�ַ���,���������Ż�����ָ��,Bits��������С
void DES_Bits2Bytes(char *dstBytes, char* srcBits, unsigned int sizeBits);

//����:Int��Bits��ת��,
//����:���任�ַ���,���������Ż�����ָ��
void DES_Int2Bits(unsigned int srcByte, char* dstBits);
		
//����:Bits��Hex��ת��
//����:���任�ַ���,���������Ż�����ָ��,Bits��������С
void DES_Bits2Hex(char *dstHex, char* srcBits, unsigned int sizeBits);
		
//����:Bits��Hex��ת��
//����:���任�ַ���,���������Ż�����ָ��,Bits��������С
void DES_Hex2Bits(char *srcHex, char* dstBits, unsigned int sizeBits);


//Ciphertext��get����
char* DES_GetCiphertextInBytes();

//Plaintext��get����
char* DES_GetPlaintext();

//��ȡ���ܺ�����ݼ�����
char* DES_GetCiphertextAnyLength();
int DES_GetCiphertextAnyLengthSize();

//��ȡ���ܺ�����ݼ�����
char* DES_GetPlaintextAnyLength();
int DES_GetPlaintextAnyLengthSize();

//����:��������Կ
//����:����PC1�任��56λ�������ַ���
//���:��������char szSubKeys[16][48]
void DES_CreateSubKey(char* sz_56key);

//����:DES�е�F����,
//����:��32λ,��32λ,key���(0-15)
//���:���ڱ任����32λ
void DES_FunctionF(char* sz_Li,char* sz_Ri,unsigned int iKey);

//����:IP�任
//����:�������ַ���,����������ָ��
//���:�����ı�ڶ�������������
void DES_InitialPermuteData(char* _src,char* _dst);

//����:����32λ������չλ48λ,
//����:ԭ32λ�ַ���,��չ�������ָ��
//���:�����ı�ڶ�������������
void DES_ExpansionR(char* _src,char* _dst);

//����:�����,
//����:�����Ĳ����ַ���1,�ַ���2,����������,����������ָ��
//���: �����ı���ĸ�����������
void DES_XOR(char* szParam1,char* szParam2, unsigned int uiParamLength, char* szReturnValueBuffer);

//����:S-BOX , ����ѹ��,
//����:48λ�������ַ���,
//���:���ؽ��:32λ�ַ���
void DES_CompressFuncS(char* _src48, char* _dst32);

//����:IP��任,
//����:���任�ַ���,����������ָ��
//���:�����ı�ڶ�������������
void DES_PermutationP(char* _src,char* _dst);

 