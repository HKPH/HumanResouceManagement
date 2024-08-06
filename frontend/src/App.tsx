import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom'; // Import BrowserRouter và các component liên quan
import LoginPage from './pages/LoginPage'; // Import các trang hoặc component khác
import HomePage from './pages/HomePage'; // Giả sử bạn có một trang HomePage

// Đảm bảo bạn bao bọc các route trong BrowserRouter
const App = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<LoginPage />} />
                <Route path="/homepage" element={<HomePage />} />
                {/* Thêm các route khác nếu có */}
            </Routes>
        </BrowserRouter>
    );
};

export default App;
