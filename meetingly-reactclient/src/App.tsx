
import { useEffect, useState } from 'react';
import './App.css'
import { Configuration, ScheduledDate, User, UsersApi } from './client'
import { Button, Fab} from '@mui/material';
import UserModal from './components/molecules/UserModal/UserModal';
import UsersTable from './components/molecules/UsersTable/UsersTable';

function App() {
  const [user, setUser] = useState<User | undefined>();
  const [users, setUsers] = useState<User[]>([]);
  const [openUserModal, setOpenUserModal] = useState(false);
  
  useEffect(() => {
    const api = new UsersApi(new Configuration({ basePath: "http://localhost:5275" }));
    api.getUsers().then((x) => { setUsers(x.data) });
  }, []);

  const handleOpenUserModal = () => setOpenUserModal(true);

  const handleCloseUserModal = () => {
    setUser(undefined);
    setOpenUserModal(false)
  };

  const handleEditUser = (user: User) => {
        setUser(user);
        setOpenUserModal(true);
  }

  return (
    <>
      <h1>Meetingly</h1>
      <Button onClick={handleOpenUserModal} variant="contained" color="success" sx={{margin: 2}}>
          Dodaj użytkownika
      </Button>
      <UsersTable users={users} setUsers={setUsers} handleEditUser={handleEditUser} />
      <UserModal open={openUserModal} onClose={handleCloseUserModal} setUsers={setUsers} user={user} key={user?.name}/>
    </>
  )
}

export default App
