// index.tsx hoặc index.js
import React from 'react';
import { createRoot } from 'react-dom/client';
import App from './App';
import './index.css';

const container = document.getElementById('root');
const root = createRoot(container!); // createRoot(container!) nếu bạn đang sử dụng TypeScript
root.render(<App />);
if (process.env.NODE_ENV === 'development') {
    // Overwrite console.error to suppress error messages
    console.error = (message) => {};
  }
  