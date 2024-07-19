import React, { useState } from 'react';
import { Form, Input, Button, Card, message } from 'antd';
import { login } from '../api/auth';
import { useCookies } from 'react-cookie';
import { useNavigate } from 'react-router-dom';

const Login = () => {
    const [loading, setLoading] = useState(false);
    const [cookies, setCookie] = useCookies(['userId']);
    const navigate = useNavigate();
    const onFinish = async (values) => {
        setLoading(true);
        try {
            const account = await login(values);
            message.success('Đăng nhập thành công');
            const userId = account.id;
            console.log('UserId:', userId);
            setCookie('userId', account.id, { path: '/' });
            setTimeout(() => {
                const savedUserId = cookies.userId;
                console.log('UserId từ cookie:', savedUserId);

                navigate('/homepage');
            }, 100);
            // Xử lý logic sau khi đăng nhập thành công
        } catch (error) {
            message.error('Đăng nhập thất bại');
            console.error('Login error:', error);
        }
        setLoading(false);
    };

    return (
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
            <Card title="Đăng nhập" style={{ width: 300 }}>
                <Form name="login" initialValues={{ remember: true }} onFinish={onFinish}>
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
                </Form>
            </Card>
        </div>
    );
};

export default Login;
