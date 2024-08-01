import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useCookies } from 'react-cookie';

const API_URL = "https://localhost:7005/api/Attachments";

const FileManagement: React.FC = () => {
  const [files, setFiles] = useState<any[]>([]);
  const [cookies] = useCookies(['userId']);
  const [selectedFile, setSelectedFile] = useState<File | null>(null);

  useEffect(() => {
    const fetchFiles = async () => {
      try {
        const response = await axios.get(API_URL);
        setFiles(response.data); // Ensure response.data.$values is an array
      } catch (error) {
        console.error('There was an error fetching the files!', error);
      }
    };

    fetchFiles();
  }, []);

  useEffect(() => {
    if (selectedFile && cookies.userId) {
      const uploadFile = async () => {
        const formData = new FormData();
        formData.append('file', selectedFile);
        formData.append('employeeId', cookies.userId.toString());

        try {
          const response = await axios.post(`${API_URL}/upload`, formData);
          const uploadedFile = response.data;
          setFiles([...files, {
            id: uploadedFile.id,
            fileName: selectedFile.name
          }]);
          setSelectedFile(null);
        } catch (error) {
          console.error('There was an error uploading the file!', error);
        }
      };

      uploadFile();
    }
  }, [selectedFile, cookies.userId, files]);

  const deleteFile = async (id: number) => {
    try {
      await axios.delete(`${API_URL}/${id}`);
      setFiles(files.filter(file => file.id !== id));
    } catch (error) {
      console.error('There was an error deleting the file!', error);
    }
  };

  return (
    <div style={{ fontFamily: 'Arial, sans-serif', padding: '20px', maxWidth: '800px', margin: '0 auto' }}>
      <h1 style={{ textAlign: 'center' }}>File Management</h1>
      
      {/* Upload File Form */}
      <div style={{ marginBottom: '20px', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
        <input
          type="file"
          onChange={(e) => {
            if (e.target.files) {
              setSelectedFile(e.target.files[0]);
            }
          }}
          style={{ marginBottom: '10px' }}
        />
      </div>
      
      {/* File List */}
      <ul style={{ listStyleType: 'none', padding: '0' }}>
        {files.map(file => (
          <li key={file.id} style={{ marginBottom: '10px', padding: '10px', border: '1px solid #ddd', borderRadius: '5px' }}>
            <div style={{ display: 'flex', alignItems: 'center' }}>
              <span style={{ flexGrow: 1 }}>{file.fileName}</span>
              <button 
                onClick={() => deleteFile(file.id)}
                style={{ backgroundColor: '#dc3545', color: '#fff', border: 'none', padding: '5px 10px', cursor: 'pointer', marginRight: '5px' }}
              >
                Delete
              </button>
              <a 
                href={`${API_URL}/download/${file.id}`} 
                download
                style={{ textDecoration: 'none', color: '#007bff' }}
              >
                Download
              </a>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default FileManagement;
