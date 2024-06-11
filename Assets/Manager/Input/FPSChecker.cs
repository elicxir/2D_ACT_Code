using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <Summary>
/// �V�[���̃t���[�����[�g���v�����ĉ�ʂɕ\������X�N���v�g�ł��B
/// </Summary>
public class FPSChecker : MonoBehaviour
{
    // �t���[�����[�g��\������e�L�X�g�ł��B
    public TextMeshProUGUI fpsText;

    // Update()���Ă΂ꂽ�񐔂��J�E���g���܂��B
    int frameCount;
    int fixedCount;

    // �O��t���[�����[�g��\�����Ă���̌o�ߎ��Ԃł��B
    float elapsedTime;
    float elapsedTime2;

    void Start()
    {

    }

    void Update()
    {
        // �Ă΂ꂽ�񐔂����Z���܂��B
        frameCount++;

        // �O�̃t���[������̌o�ߎ��Ԃ����Z���܂��B
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1.0f)
        {
            // �o�ߎ��Ԃ�1�b�𒴂��Ă�����A�t���[�����[�g���v�Z���܂��B
            float fps = 1.0f * frameCount / elapsedTime;

            // �v�Z�����t���[�����[�g����ʂɕ\�����܂��B(�����_�ȉ�2�P�^�܂�)
            string fpsRate = $"FPS: {fps.ToString("F2")} Fixed:{fps2.ToString("F2")}";
            fpsText.SetText(fpsRate);

            // �t���[���̃J�E���g�ƌo�ߎ��Ԃ����������܂��B
            frameCount = 0;
            elapsedTime = 0f;
        }
    }


    float  fps2;
    private void FixedUpdate()
    {
        // �Ă΂ꂽ�񐔂����Z���܂��B
        fixedCount++;

        // �O�̃t���[������̌o�ߎ��Ԃ����Z���܂��B
        elapsedTime2 += Time.deltaTime;

        if (elapsedTime2 >= 1.0f)
        {
            // �o�ߎ��Ԃ�1�b�𒴂��Ă�����A�t���[�����[�g���v�Z���܂��B
             fps2 = 1.0f * fixedCount / elapsedTime2;

            // �t���[���̃J�E���g�ƌo�ߎ��Ԃ����������܂��B
            fixedCount = 0;
            elapsedTime2 = 0f;
        }
    }
}