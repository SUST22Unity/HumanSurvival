using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionTile : MonoBehaviour
{
    public float x;   //Ÿ�� ���� ũ��
    public float y;   //Ÿ�� ���� ũ��
    public int probability;
    public GameObject prefab;   //�ҷ��� ������
    public GameObject respawn;   //���� ������
    // ��ũ Area���� �浹���� ����� ���� �ҷ����� �Լ�
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) { return; }  //Area �������� ��� �͸� �Ʒ� �ڵ尡 �����

        Vector3 playerPos = GameManager.instance.player.transform.position; //���ΰ� ��ġ
        Vector3 myPos = transform.position; //���� Tilemap ��ġ
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.Movement;  //���ΰ� �̵����� ����
        float dirX = playerDir.x > 0 ? 1 : -1;
        float dirY = playerDir.y > 0 ? 1 : -1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)  //x������ ���� �̵���
                {
                    RemoveObject(collision);
                    transform.Translate(Vector3.right * dirX * x * 2); //���ΰ� �̵� ���� �տ� tilemap�� ���� ���� x*2 ��ŭ �̵�
                    ObjectRespown(transform.position);
                }
                else if (diffX < diffY) //y������ ���� �̵���
                {
                    RemoveObject(collision);
                    transform.Translate(Vector3.up * dirY * y * 2); //���ΰ� �̵� ���� �տ� tilemap�� ���� ���� y*2 ��ŭ �̵�
                    ObjectRespown(transform.position);
                }
                break;
        }
    }
    private void ObjectRespown(Vector3 myPos) {  //������ ����
        if (respawn == null & (Random.Range(0.0f,100.0f) <= probability))   //probability Ȯ���� ����
        {
            float randomX = Random.Range(-x/2, x/2); //���� X��ǥ
            float randomY = Random.Range(-y/2, y/2); //���� Y��ǥ
            //instantiate�Լ� (������Ʈ �̸�, ������Ʈ ��ġ, ������Ʈ ȸ�� ��)
            respawn = Instantiate(prefab,new Vector3(myPos.x+randomX,myPos.y+randomY,0f) , Quaternion.identity);
        }
    }
    private void RemoveObject(Collider2D collision) {   //������ ����
        //if (collision.gameObject.tag == "Object")
        Destroy(respawn);
    }
}