1.�l�b�g���[�N�\��
�@TCP�̃N���C�A���g�E�T�[�o�Ƃ���B�e�Ӗ��͈ȉ��̂Ƃ���B
�@�@�T�[�o�@�@�@�F����񂯂�̎���o���l�i�e�l�쐬, Sample�j
�@�@�N���C�A���g�F����񂯂�̎���T�[�o�ɕ����iReferee)

�@�e�l�̍�����T�[�o���e�l�i�܂��͒N���j��PC�œ������B
�@�N���C�A���g�͒N����PC���g�p����B

2.��
�@����񂯂�̎���ȉ��̐��l�ŕ\��
�@�@�薳�� = -1
    �O�[   = 0
    �`���L = 1
    �p�[�@ = 2
�@��ʂ̃��[���ǂ���̏��s�Ƃ���B�������薳�����o�����ꍇ�A�o�����ق��̕����ƂȂ�
�@�݂��Ɏ薳���̏ꍇ�A���������Ƃ���B

3.�T�[�o�[�N���C�A���g�Ԃ̒ʐM
�E�e64Byte�̃��b�Z�[�W�Ƃ���B
�E���e�͂��ׂ�ASCII�����ŁAC�̊֐��Ăяo���𕶎���œn���B
�E64byte�ɑ���Ȃ�����0�Ŗ��߂�B
�E�T�[�o�̎����֐��͈ȉ��̂T��
�@�Estring GetName()
�@�Eint GetFirstHand(int times)
�@�Eint GetSecondHand(int times, int opp1st)
�@�Eint GetThirdHand(int times, int opp2nd)
�@�Evoid SetResult(int time, int result, int opp3rd) 

�E�n����镶����̗�
  "?=GetThirdHand(99, 0);"  // 99���opp2nd=�O�[�̏ꍇ
�@�@���擪��"?="�͖߂�l����������B�i�߂�lvoid�̏ꍇ�A�Ȃ��j
�@�@��������";"�͏I�[�i�K�{�j

�EC����ŉ\�ȃX�y�[�X�͂��Ă��悢�i�����ԂȂǁj
�E�ԓ��͖߂�l�݂̂𕶎���ŕԂ�(������64byte)
�@"2"			�@ // �p�[
�E������100ms�ȓ��ɕԂ��B100ms�𒴂����ꍇ�薳���Ƃ���B

4.���s
 SetResult�ŏ��s���n�����B���s�̒l�͈ȉ��̂Ƃ���B
�@�@���������F-2�@
�@�@�����@�@�F-1
�@�@���������F0
�@�@�����@�@�F1
�@�@���������F2

�@�������@3��ڂɏo�����̂�1��ڂ�2��ڂɂȂ���Ƃ��B

5.�n�[�h�E�F�A/�\�t�g�E�F�A
�@��L�����삷��Ȃ猾��, OS, �n�[�h�E�F�A������Ȃ��B
  C�ł�Perl�ł�Ruby�ł��BC#�i�T���v�������j�ł�OK
�@���U�������Ă��APC1000��g���Ă��A�N���E�h�ł�OK�B�@

6.�T���v���ɂ���
 6.1 �R���p�C��
�@ Visual Stuido 2010 C# Express�ŃR���p�C���E����m�F���s�����B

 6.2 �g����
 �@�@�R�}���h�v�����v�g�R�����グ��B
 �@�A�T�[�o(SampleJankenAgent)���Q�A�ȉ��̈����t���ŋN��
   �@SampleJankenAgent.exe �|�[�g
    �@��: SampleJankenAgent.exe 49999
�@ �B�N���C�A���g���P�A�ȉ��̈����ŋN��
   �@JankenReferee.exe �{�s�� �T�[�o�PIP�A�h���X �T�[�o�P�|�[�g �T�[�o2IP�A�h���X �T�[�o2�|�[�g
    �@�� JankenReferee.exe 1000 127.0.0.1 49999 127.0.0.1 50000

7.�Č�
�@JankenReferee���g���Đ���ɓ��삷�邱�ƁB
�@SampleJankenAgent�Ɋm���ɏ����̂��쐬���邱�ƁB���������s�񐔂�1000��i�b��j�Ƃ��A
�@�m���ɏ��Ƃ͎{�s�񐔂ɂ����āA�m���ɏ����̐��������̐������邱�ƂƂ���B
�@

8.�ǋL
�@RPC�̃v���g�R���K���ɍ�����B�{����SOAP�Ƃ�XMLRPC�Ƃ��ɂ����������B
�@ 