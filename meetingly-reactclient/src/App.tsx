
import { useEffect, useState } from 'react';
import './App.css'
import { Configuration, ScheduledDate, User, UsersApi } from './client'
import { DataGrid, GridColDef, GridRemoveIcon } from '@mui/x-data-grid'
import EditIcon from '@mui/icons-material/Edit';
import { Button, Fab} from '@mui/material';
import UserModal from './components/molecules/UserModal/UserModal';

function App() {
  const [users, setUsers] = useState<User[]>([]);
  const [openUserModal, setOpenUserModal] = useState(false);
  useEffect(() => {
    const api = new UsersApi(new Configuration({ basePath: "http://localhost:5275" }));
    api.getUsers().then((x) => { setUsers(x.data) });
  }, []);

  const columns: GridColDef[] = [
    { field: 'id', headerName: 'ID', width: 150 },
    { field: 'name', headerName: 'Name', width: 150 },
    { field: 'scheduledDates', headerName: 'Scheduled Dates', width: 150,
    valueGetter: (params) => (params.row.scheduledDates as ScheduledDate[]).length},
    {
      field: 'actions',
      headerName: 'Akcje',
      width: 150,
      renderCell: (params) => (
        <div style={{display: "flex", gap: "5px"}}>
          <Fab color="error" size="small" aria-label="add" onClick={() => deleteUser(params.row.id)}>
            <GridRemoveIcon />
          </Fab>
          <Fab color="secondary" size="small" aria-label="edit">
            <EditIcon />
          </Fab>
        </div>
      ),
    },
  ];

  const deleteUser = (userId: number) => {
    const api = new UsersApi(new Configuration({ basePath: "http://localhost:5275" }));
    api.deleteUser(userId)
    .then(() => {
        setUsers(prevUsers => prevUsers.filter(user => user.id !== userId));
    })
    .catch(error => {
        console.error("Error deleting user:", error);
    });
  };

  


  const handleOpenUserModal = () => setOpenUserModal(true);
  const handleCloseUserModal = () => setOpenUserModal(false);

  

  return (
    <>
      <h1>Meetingly</h1>
      <div>
        <Button onClick={handleOpenUserModal} variant="contained" color="success" sx={{margin: 2}}>
          Dodaj użytkownika
        </Button>
        <DataGrid rows={users} columns={columns} />
      </div>
      <UserModal open={openUserModal} onClose={handleCloseUserModal} setUsers={setUsers}/>
    </>
  )
}

export default App
