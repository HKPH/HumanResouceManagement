import React, { useEffect, useState } from 'react';
import { Button, Table, Popconfirm, notification, Modal, Form, Input, Switch } from 'antd';
import { EditOutlined, DeleteOutlined, PlusOutlined } from '@ant-design/icons';
import { getBanks, createBank, updateBank, deleteBank } from '../api/bankApi';

const BankList: React.FC = () => {
  const [banks, setBanks] = useState<any[]>([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isEditMode, setIsEditMode] = useState(false);
  const [editingBank, setEditingBank] = useState<any>(null);
  const [form] = Form.useForm();
  const [filterActive, setFilterActive] = useState(true); // Đặt mặc định là true

  useEffect(() => {
    fetchBanks();
  }, [filterActive]);

  const fetchBanks = async () => {
    try {
      const data = await getBanks(filterActive);
      console.log('Fetched banks data:', data);
      const banksData = data.$values;
      if (Array.isArray(banksData)) {
        const banksWithIndex = banksData.map((bank, index) => ({
          ...bank,
          key: bank.id,
          stt: index + 1 // Thêm thuộc tính STT
        }));
        setBanks(banksWithIndex);
      } else {
        setBanks([]);
      }
    } catch (error) {
      notification.error({ message: 'Failed to fetch banks' });
    }
  };

  const handleAddBank = async (values: any) => {
    try {
      if (isEditMode && editingBank) {
        const updatedBank = { ...editingBank, ...values }; // Kết hợp id với các trường được chỉnh sửa
        await updateBank(editingBank.id, updatedBank);
        notification.success({ message: 'Bank updated successfully' });
      } else {
        await createBank(values);
        notification.success({ message: 'Bank added successfully' });
      }
      setIsModalVisible(false);
      form.resetFields();
      fetchBanks();
    } catch (error) {
      notification.error({ message: `Failed to ${isEditMode ? 'update' : 'add'} bank` });
    }
  };

  const handleEdit = (record: any) => {
    setIsEditMode(true);
    setEditingBank(record);
    form.setFieldsValue({
      name: record.name,
      description: record.description,
      address: record.address,
      phoneNumber: record.phoneNumber,
      email: record.email,
      web: record.web,
      active: record.active, // Set giá trị cho active
    });
    setIsModalVisible(true);
  };

  const handleDelete = async (id: number) => {
    try {
      await deleteBank(id);
      notification.success({ message: 'Bank deleted successfully' });
      fetchBanks();
    } catch (error) {
      notification.error({ message: 'Failed to delete bank' });
    }
  };

  const handleFilterActiveBanks = () => {
    setFilterActive(!filterActive);
  };

  const columns = [
    { title: 'STT', dataIndex: 'stt', key: 'stt' }, // Cột STT
    { title: 'Name', dataIndex: 'name', key: 'name' },
    { title: 'Description', dataIndex: 'description', key: 'description' },
    { title: 'Address', dataIndex: 'address', key: 'address' },
    { title: 'Phone Number', dataIndex: 'phoneNumber', key: 'phoneNumber' },
    { title: 'Email', dataIndex: 'email', key: 'email' },
    { title: 'Website', dataIndex: 'web', key: 'web' }, // Hiển thị thông tin website
    { title: 'Active', dataIndex: 'active', key: 'active', render: (text: boolean) => (text ? 'Yes' : 'No') },
    {
      title: 'Action',
      key: 'action',
      render: (text: any, record: any) => (
        <>
          <Button
            icon={<EditOutlined />}
            onClick={() => handleEdit(record)}
            style={{ marginRight: 8 }}
          >
            Edit
          </Button>
          <Popconfirm title="Are you sure to delete?" onConfirm={() => handleDelete(record.id)}>
            <Button icon={<DeleteOutlined />}>Delete</Button>
          </Popconfirm>
        </>
      ),
    },
  ];

  return (
    <div>
      <Button
        type="primary"
        icon={<PlusOutlined />}
        onClick={() => {
          setIsEditMode(false);
          setIsModalVisible(true);
        }}
        style={{ marginBottom: 16 }}
      >
        Thêm Ngân Hàng
      </Button>
      <Switch
        checked={filterActive}
        onChange={handleFilterActiveBanks}
        checkedChildren="Active Banks"
        unCheckedChildren="All Banks"
        style={{ marginBottom: 16, marginLeft: 16 }}
      />
      <Table
        dataSource={banks}
        columns={columns}
        rowKey="id" // Sử dụng id làm khóa duy nhất cho hàng
      />
      <Modal
        title={isEditMode ? 'Chỉnh sửa Ngân Hàng' : 'Thêm Ngân Hàng'}
        open={isModalVisible}
        onCancel={() => setIsModalVisible(false)}
        footer={null}
      >
        <Form
          form={form}
          onFinish={handleAddBank}
          layout="vertical"
        >
          <Form.Item
            name="name"
            label="Name"
            rules={[{ required: true, message: 'Please input the bank name!' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="description"
            label="Description"
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="address"
            label="Address"
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="phoneNumber"
            label="Phone Number"
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="email"
            label="Email"
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="web"
            label="Web"
          >
            <Input />
          </Form.Item>
          {isEditMode && (
            <Form.Item
              name="active"
              label="Active"
              valuePropName="checked" // Set giá trị cho Switch
            >
              <Switch />
            </Form.Item>
          )}
          <Form.Item>
            <Button type="primary" htmlType="submit">
              {isEditMode ? 'Update Bank' : 'Add Bank'}
            </Button>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default BankList;
