1.�l�b�g���[�N�\��
�@TCP�̃N���C�A���g�E�T�[�o�Ƃ���B�e�Ӗ��͈ȉ��̂Ƃ���B
�@�@�T�[�o�@�@�@�F����񂯂�̎���o���l�i�e�l�쐬, Sample�j
�@�@�N���C�A���g�F����񂯂�̎���T�[�o�ɕ����iReferee)

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
�EC����ŉ\�ȃX�y�[�X�͂��Ă��悢�i�����ԂȂǁj
�E�ԓ��͖߂�l�݂̂𕶎���ŕԂ�(������64byte)
�@"2"			�@ // �p�[
�E������100ms�ȓ��ɕԂ��B100ms�𒴂����ꍇ�薳���Ƃ���B

4.�n�[�h�E�F�A
�@��L�����삷��Ȃ猾��A�n�[�h�E�F�A������Ȃ��B
