
#include <stdio.h>
#include <memory.h>
#include <malloc.h>
#include <string.h>
#include "DES2.h"
void Byte2Hex(char *dstHex, char* srcBits, unsigned int sizeBits);
void Hex2Byte(char *Hex, char *bytes, unsigned int size);

void main()
{
	unsigned char key[9] =  {'1','2','3','4','5','6','7','8','\0'}; //������Կ
	unsigned char *data="abcdefg123478";
	unsigned char *DesBuf,*Hex,*Data2;int iSize=0;
	char *HexStr=0;unsigned int iHexLen;
	int i=0;
	

	printf("��Կ��%s\n",key);

	DES_Initialize(); //��ʼ��DES��
	DES_InitializeKey(key);//��ʼ����Կ
	
	//*************************����*********************
	DES_EncryptAnyLength(data,15); //���ݼ���
	DesBuf=DES_GetCiphertextAnyLength();//ȡ�����ܵ�����
	iSize=DES_GetCiphertextAnyLengthSize();//ȡ�����ܺ����ݳ���
	
	//�����ʾ
	Hex= (char *)malloc(iSize*2+1);
	Byte2Hex(Hex,DesBuf,iSize);
	printf("���ܺ����ݣ�%s\n",Hex);
	free(Hex);
	//**************************************************

	//****************����*********************
	data=0;
	DES_DecryptAnyLength(DesBuf,iSize);
	data=DES_GetPlaintextAnyLength();
	iSize=DES_GetPlaintextAnyLengthSize();
	printf("���ܺ����ݣ�%s�����س��ȣ�%d\n",data,iSize);
	//*****************************************
	printf("\n");
	DES_Initialize(); //��������Ķ�̬�ռ�



	//ת��ʮ����������Ϊ�ֽ�����
	HexStr = "A0AAEE30BB16E2D86ADCF925E2772B8A";
	iHexLen=strlen(HexStr);
	DesBuf= (char *)malloc(iHexLen/2);
	Hex2Byte(HexStr,DesBuf,iHexLen);
	
	//����
	DES_Initialize(); //��ʼ��DES��
	DES_InitializeKey(key);//��ʼ����Կ
	DES_DecryptAnyLength(DesBuf,iHexLen/2);
	DesBuf=DES_GetPlaintextAnyLength();
	iSize=DES_GetPlaintextAnyLengthSize();
	
	Data2= (char *)malloc(iSize);
	memcpy(Data2,DesBuf,iSize);
	DES_Initialize(); //��������Ķ�̬�ռ�

	//�����ʾ
	Hex= (char *)malloc(iSize*2+1);
	Byte2Hex(Hex,Data2,iSize);
	printf("���ܺ����ݣ�%s\n",Hex);
	free(Hex);
	
	free(Data2);

}

void Byte2Hex(char *dstHex, char* srcBytes, unsigned int size)
{
	unsigned int i=0,j=0,index=0;
	char *mHexDigits="0123456789ABCDEF";
	memset(dstHex,0,size*2+1);
	for(i=0; i < size; i++) 
	{
		dstHex[j] =mHexDigits[(unsigned char)srcBytes[i] / 16]; j++;
		dstHex[j] =mHexDigits[(unsigned char)srcBytes[i] % 16];  j++;
	}

}

void Hex2Byte(char *Hex, char *bytes, unsigned int size)
{
	//size--ʮ�������ַ�������
	unsigned char mByteDigits[256]="",*tmp,bData;
	unsigned int i=0,j=0;
	
	//���������
	tmp="0123456789ABCDEF";
	for(i=0;i<16;i++)
	{
		mByteDigits[tmp[i]]=i;
	}
	tmp="abcdef";
	for(i=0;i<6;i++)
	{
		mByteDigits[tmp[i]]=i+10;
	}

	memset(bytes,0,size/2);

	for(i=0;i<size;i++)
	{
		bData = mByteDigits[Hex[i]]*16;
		i++;
		bData+=mByteDigits[Hex[i]];

		bytes[j]=bData;
		j++;
	}
}