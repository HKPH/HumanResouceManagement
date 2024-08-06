import React, { useEffect, useState, useMemo } from 'react';
import { Input, Tree, Modal, message, Switch, Button, Select, Form } from 'antd';
import type { TreeDataNode } from 'antd';
import { getDepartments, getActiveDepartments, createDepartment, updateDepartment, deleteDepartment, Department } from '../api/departmentApi';

const { Option } = Select;

const DepartmentList: React.FC = () => {
  const [departments, setDepartments] = useState<TreeDataNode[]>([]);
  const [expandedKeys, setExpandedKeys] = useState<React.Key[]>([]);
  const [autoExpandParent, setAutoExpandParent] = useState(true);
  const [selectedDepartment, setSelectedDepartment] = useState<Department | null>(null);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [showActive, setShowActive] = useState(true);
  const [form] = Form.useForm();
  const [createForm] = Form.useForm();
  const [isCreateModalVisible, setIsCreateModalVisible] = useState(false);

  useEffect(() => {
    fetchDepartments();
  }, [showActive]);

  const fetchDepartments = async () => {
    try {
      const response = showActive ? await getActiveDepartments() : await getDepartments();
      console.log('API Response:', response);  
      const data = response as Department[];  
      console.log('Data:', data);  
      const treeData = transformToTreeData(data);
      console.log('Tree Data:', treeData); 
      setDepartments(treeData);
    } catch (error) {
      console.error('Failed to fetch departments:', error); 
      message.error('Failed to fetch departments');
    }
  };

  const transformToTreeData = (data: Department[]): TreeDataNode[] => {
    const map = new Map<number, TreeDataNode>();

    data.forEach(dept => {
      map.set(dept.id, { title: dept.name, key: dept.id.toString(), children: [] });
    });

    const treeData: TreeDataNode[] = [];

    data.forEach(dept => {
      const node = map.get(dept.id);
      if (dept.parentDepartmentId) {
        const parent = map.get(dept.parentDepartmentId);
        parent?.children?.push(node!);
      } else {
        treeData.push(node!);
      }
    });

    return treeData;
  };

  const generateList = (data: TreeDataNode[], parentId: number | null = null): { key: React.Key; title: string; id: number; parentDepartmentId: number | null }[] => {
    const result: { key: React.Key; title: string; id: number; parentDepartmentId: number | null }[] = [];

    for (let i = 0; i < data.length; i++) {
      const node = data[i];
      const { key, title, children } = node;
      result.push({ 
        key, 
        title: title as string, 
        id: parseInt(key.toString()), 
        parentDepartmentId: parentId 
      });
      if (children) {
        result.push(...generateList(children, parseInt(key.toString())));
      }
    }

    return result;
  };

  const dataList = generateList(departments);

  const getParentKey = (key: React.Key, tree: TreeDataNode[]): React.Key | null => {
    let parentKey: React.Key | null = null;
    for (let i = 0; i < tree.length; i++) {
      const node = tree[i];
      if (node.children) {
        if (node.children.some((item) => item.key === key)) {
          parentKey = node.key;
        } else if (getParentKey(key, node.children)) {
          parentKey = getParentKey(key, node.children);
        }
      }
    }
    return parentKey;
  };

  const onExpand = (newExpandedKeys: React.Key[]) => {
    setExpandedKeys(newExpandedKeys);
    setAutoExpandParent(false);
  };

  const treeData = useMemo(() => {
    const loop = (data: TreeDataNode[]): TreeDataNode[] =>
      data.map((item) => {
        const strTitle = item.title as string;
        const title = <span key={item.key}>{strTitle}</span>;
        if (item.children) {
          return { title, key: item.key, children: loop(item.children) };
        }

        return {
          title,
          key: item.key,
        };
      });

    return loop(departments);
  }, [departments]);

  const handleSelect = (selectedKeys: React.Key[], info: any) => {
    const selectedId = parseInt(selectedKeys[0].toString());
    const selectedDepartment = departments.flatMap(dept => {
      return dept.children ? [dept, ...dept.children] : [dept];
    }).find(dept => dept.key === selectedKeys[0]);

    if (selectedDepartment) {
      const parentDeptId = dataList.find(dept => dept.key === getParentKey(selectedKeys[0], departments))?.id ?? null;

      setSelectedDepartment({
        id: selectedId,
        name: selectedDepartment.title as string,
        active: true,  // Điều chỉnh nếu cần
        parentDepartmentId: parentDeptId
      });
      setIsModalVisible(true);
    }
  };

  const handleOk = () => {
    setIsModalVisible(false);
  };

  const handleCancel = () => {
    setIsModalVisible(false);
  };

  const handleDelete = async () => {
    if (selectedDepartment) {
      try {
        await deleteDepartment(selectedDepartment.id);
        message.success('Department deleted successfully');
        fetchDepartments();
        setIsModalVisible(false);
      } catch (error) {
        message.error('Failed to delete department');
      }
    }
  };

  const handleUpdate = async () => {
    try {
      const values = await form.validateFields();
      await updateDepartment({ ...selectedDepartment, ...values });
      message.success('Department updated successfully');
      fetchDepartments();
      setIsModalVisible(false);
    } catch (error) {
      message.error('Failed to update department');
    }
  };

  const handleCreate = async () => {
    try {
      const values = await createForm.validateFields();
      await createDepartment(values);
      message.success('Department created successfully');
      fetchDepartments();
      setIsCreateModalVisible(false);
      createForm.resetFields();
    } catch (error) {
      message.error('Failed to create department');
    }
  };

  useEffect(() => {
    if (selectedDepartment) {
      form.setFieldsValue({
        name: selectedDepartment.name,
        parentDepartmentId: selectedDepartment.parentDepartmentId ?? null, // Sửa thành null
        active: selectedDepartment.active,
      });
    }
  }, [selectedDepartment]);

  return (
    <div>
      <div style={{ marginBottom: 8 }}>
        <Switch checked={showActive} onChange={() => setShowActive(!showActive)} /> Show Active Only
      </div>
      <Button type="primary" onClick={() => setIsCreateModalVisible(true)} style={{ marginBottom: 8 }}>
        Create Department
      </Button>
      <Tree
        onExpand={onExpand}
        expandedKeys={expandedKeys}
        autoExpandParent={autoExpandParent}
        treeData={treeData}
        onSelect={handleSelect}
      />
      <Modal
        title="Department Details"
        visible={isModalVisible}
        onOk={handleUpdate}
        onCancel={handleCancel}
        footer={[
          <Button key="back" onClick={handleCancel}>Cancel</Button>,
          <Button key="submit" type="primary" onClick={handleUpdate}>Update</Button>,
          <Button key="delete" danger onClick={handleDelete}>Delete</Button>,
        ]}
      >
        {selectedDepartment && (
          <Form form={form} layout="vertical">
            <Form.Item name="name" label="Name" rules={[{ required: true, message: 'Please input the name!' }]}>
              <Input />
            </Form.Item>
            <Form.Item name="parentDepartmentId" label="Parent Department">
              <Select>
                <Option value={null}>None</Option>
                {dataList.map(dept => (
                  <Option key={dept.key} value={dept.id}>
                    {dept.title}
                  </Option>
                ))}
              </Select>
            </Form.Item>
            <Form.Item name="active" label="Active" valuePropName="checked">
              <Switch />
            </Form.Item>
          </Form>
        )}
      </Modal>
      <Modal
        title="Create Department"
        visible={isCreateModalVisible}
        onOk={handleCreate}
        onCancel={() => setIsCreateModalVisible(false)}
        footer={[
          <Button key="back" onClick={() => setIsCreateModalVisible(false)}>Cancel</Button>,
          <Button key="submit" type="primary" onClick={handleCreate}>Create</Button>,
        ]}
      >
        <Form form={createForm} layout="vertical">
          <Form.Item name="name" label="Name" rules={[{ required: true, message: 'Please input the name!' }]}>
            <Input />
          </Form.Item>
          <Form.Item name="parentDepartmentId" label="Parent Department">
            <Select>
              <Option value={null}>None</Option>
              {dataList.map(dept => (
                <Option key={dept.key} value={dept.id}>
                  {dept.title}
                </Option>
              ))}
            </Select>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default DepartmentList;