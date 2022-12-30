using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;

public class DatabaseAccess : MonoBehaviour
{
    TestInputStr testInputStr = null;

    // DataBase : Collection�� ������ �����̳�
    // Collection : Document�� �׷�, Document�� ���ο� ��ġ�� ����
    // Document : �Ѱ� �̻��� Key-value ������ �̷���� ����
    // Key / Field : �÷� ��� ���� ��
    // projection : Document�� ��ȸ�� �� ������ Field�� ����

    #region Client �ּ� ����
    MongoClient client = new MongoClient("mongodb+srv://MetaTrand:dkagh@cluster0.yytmq3i.mongodb.net/?retryWrites=true&w=majority");
    #endregion

    IMongoDatabase database = null;
    IMongoCollection<BsonDocument> collection;

    private void Awake()
    {

    }

    void Start()
    {
        testInputStr = FindObjectOfType<TestInputStr>();
        BsonComunication();
    }

    public void BsonComunication()
    {
        #region Database ����
        // MongoDB database name
        database = client.GetDatabase("Test");
        #endregion

        #region Collection ����
        // �ش� Database�� �ִ� Collection ��������
        // MongoDB collection name
        collection = database.GetCollection<BsonDocument>("TestCollection");
        #endregion

        #region ������ �߰�
        ////�����ͺ��̽� �߰�
        BsonDocument document = new BsonDocument { { "name", "����" }, { "age", 26 }, { "tall", 123} };
        //collection.InsertOne(document);
        #endregion

        #region �����Ϳ��� �ش� ���� ����
        var fillter = Builders<BsonDocument>.Filter.Eq("ID", "������ʹ�");
        #endregion

        #region ���ǿ� �ش��ϴ� ������ �ҷ�����
        var callName = collection.Find(fillter).FirstOrDefault();
        #endregion

        // ���ǿ� �����ϴ� �ڷᰡ ���� �� = null; 
        if (callName == null) Debug.Log("��");

        #region �����ͺ��̽� ���� ����
        // �ϳ��� ������ ���̽��� �����ؼ� �ٸ� ���� ���� ����
        // ex) name���� ���� �� age ���� ����
        var update = Builders<BsonDocument>.Update.Set("PW", "ABC");
        collection.UpdateOne(fillter, update);
        #endregion

        #region �����ͺ��̽� ���� �߰�
        // fillter�������� �˻��� �����Ϳ� nickname�̶�� key�� �����ڶ�� value �߰�
        var updateSet = Builders<BsonDocument>.Update.Set("ID", "������ʹ�");
        collection.UpdateOne(fillter, updateSet);
        #endregion

        Debug.Log(callName.ToString());

        #region Document�� �����Ͽ� ���ϴ� Value ���
        var nullFillter = collection.Find(fillter).FirstOrDefault();//if null �̸� ã�� ����
        if (nullFillter != null)
        {
            Debug.Log(nullFillter.GetValue("PW"));
        }
        #endregion

        string IDInput = nullFillter.GetValue("ID").ToString();
        string PWInput = nullFillter.GetValue("PW").ToString();

        testInputStr.InputText(IDInput, PWInput);
    }
}
