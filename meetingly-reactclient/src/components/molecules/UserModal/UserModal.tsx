import { Modal, Box, Typography, FormControl, TextField, Button } from '@mui/material';
import { Configuration, User, UsersApi } from '../../../client';
import { useState } from 'react';


export interface UserModalProps {
    open: boolean;
    onClose: () => void;
    setUsers: React.Dispatch<React.SetStateAction<User[]>>;
}

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
    display: "flex",
    flexDirection: "column",
    gap: "10px",
    alignItems: "center"
  };

const UserModal: React.FC<UserModalProps> = ({ open, onClose, setUsers }) => {
    const [name, setName] = useState<string>("");
    const addUser = () => {
        const api = new UsersApi(new Configuration({ basePath: "http://localhost:5275" }));
        api.addUser({name: name})
        .then((x) => {
            setUsers(prevUsers => [...prevUsers, x.data]);
            setName("");
            onClose();
        })
        .catch(error => {
            console.error("Error deleting user:", error);
        });
      };
    return (
        <Modal
            open={open}
            onClose={onClose}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description"
        >
            <Box sx={style}>
                <Typography id="modal-modal-title" variant="h6" component="h2">
                    Dodaj użytkownika
                </Typography>
                <TextField type="text" variant='outlined' placeholder="Imię" onChange={(e) => setName(e.target.value)} value={name}/>
                <Button onClick={addUser} variant="contained" color="success">
                    Dodaj
                </Button>
            </Box>
        </Modal>
    );
}

export default UserModal;