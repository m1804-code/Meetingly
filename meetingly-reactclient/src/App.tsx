
import { useState } from 'react';
import './App.css'
import { Configuration, User, UsersApi } from './client'
import { DataGrid, GridColDef, GridRowsProp } from '@mui/x-data-grid'

function App() {
  const api = new UsersApi(new Configuration({ basePath: "http://localhost:5275" }));
  const [users, setUsers] = useState<User[]>([]);
  api.getUsers().then((x) => { setUsers(x.data) });

  const rows: GridRowsProp = [
    { id: 1, col1: 'Hello', col2: 'World' },
    { id: 2, col1: 'DataGridPro', col2: 'is Awesome' },
    { id: 3, col1: 'MUI', col2: 'is Amazing' },
  ];

  const columns: GridColDef[] = [
    { field: 'col1', headerName: 'Column 1', width: 150 },
    { field: 'col2', headerName: 'Column 2', width: 150 },
  ];

  return (
    <>
      <div style={{ height: 300, width: '100%' }}>
        <DataGrid rows={rows} columns={columns} />
      </div>
    </>
  )
}

export default App
