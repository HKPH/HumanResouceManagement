import React, { useState } from 'react';
import { Form, Input, Button, Card, message, Modal } from 'antd';
import { login } from '../api/auth';
import { createAccount } from '../api/accountApi';

import { useCookies } from 'react-cookie';
import { useNavigate } from 'react-router-dom';
import { createEmployee } from '../api/employeeApi'; 

const Login: React.FC = () => {
    const [loading, setLoading] = useState(false);
    const [cookies, setCookie] = useCookies(['userId']);
    const [isRegistering, setIsRegistering] = useState(false);
    const navigate = useNavigate();

    const onFinishLogin = async (values: any) => {
        setLoading(true);
        try {
            const account = await login(values);
            message.success('Đăng nhập thành công');
            const userId = account.employeeId;
            setCookie('userId', account.employeeId, { path: '/' });
            setTimeout(() => {
                navigate('/homepage');
            }, 100);
        } catch (error) {
            message.error('Đăng nhập thất bại');
            console.error('Login error:', error);
        }
        setLoading(false);
    };

    const onFinishRegister = async (values: any) => {
        setLoading(true);
        try {
            const employeeResponse = await createEmployee();
            const employeeId = employeeResponse.id;

            const accountDto = {
                username: values.username,
                password: values.password,
                role: "admin",
                employeeId: employeeId,
            };
            await createAccount(accountDto);

            message.success('Tạo tài khoản thành công! Vui lòng đăng nhập.');
            setIsRegistering(false);
        } catch (error) {
            message.error('Tạo tài khoản thất bại');
            console.error('Registration error:', error);
        }
        setLoading(false);
    };

    return (
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
            <Card title="Đăng nhập" style={{ width: 300 }}>
                <Form name="login" initialValues={{ remember: true }} onFinish={onFinishLogin}>
                    <Form.Item name="username" rules={[{ required: true, message: 'Vui lòng nhập tên đăng nhập!' }]}>
                        <Input placeholder="Tên đăng nhập" />
                    </Form.Item>

                    <Form.Item name="password" rules={[{ required: true, message: 'Vui lòng nhập mật khẩu!' }]}>
                        <Input.Password placeholder="Mật khẩu" />
                    </Form.Item>

                    <Form.Item>
                        <Button type="primary" htmlType="submit" loading={loading} block>
                            Đăng nhập
                        </Button>
                    </Form.Item>

                    <Form.Item>
                        <Button type="link" onClick={() => setIsRegistering(true)}>
                            Đăng ký tài khoản
                        </Button>
                    </Form.Item>
                </Form>
            </Card>

            <Modal
                title="Đăng ký tài khoản"
                visible={isRegistering}
                onCancel={() => setIsRegistering(false)}
                footer={null}
                centered
            >
                <Form name="register" initialValues={{ remember: true }} onFinish={onFinishRegister}>
                    <Form.Item name="username" rules={[{ required: true, message: 'Vui lòng nhập tên đăng nhập!' }]}>
                        <Input placeholder="Tên đăng nhập" />
                    </Form.Item>

                    <Form.Item name="password" rules={[{ required: true, message: 'Vui lòng nhập mật khẩu!' }]}>
                        <Input.Password placeholder="Mật khẩu" />
                    </Form.Item>

                    <Form.Item>
                        <Button type="primary" htmlType="submit" loading={loading} block>
                            Tạo tài khoản
                        </Button>
                    </Form.Item>
                </Form>
            </Modal>
        </div>
    );
};

export default Login;
