import { Modal, Box, Typography, FormControl, TextField, Button } from '@mui/material';
import { Configuration, User, UsersApi } from '../../../client';
import { useState } from 'react';


export interface UserModalProps {
    open: boolean;
    onClose: () => void;
    setUsers: React.Dispatch<React.SetStateAction<User[]>>;
    user?: User;
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

const UserModal: React.FC<UserModalProps> = ({ open, onClose, setUsers, ...props }) => {
    const [user, setUser] = useState<User>(props.user ?? {name: ""});
    const handleClickSave = () => {
        const api = new UsersApi(new Configuration({ basePath: "http://localhost:5275" }));

        if (user.id) {
                api.updateUser(user.id, {name: user.name})
                .then(() => {
                    setUsers(prevUsers => prevUsers.map(u => u.id === user.id ? user : u));
                    handleOnClose();
                })
                .catch(error => {
                    console.error("Error updating user:", error);
                });
        }
        else {
                api.addUser({name: user.name})
                    .then((x) => {
                        setUsers(prevUsers => [...prevUsers, x.data]);
                        handleOnClose();
                    })
                    .catch(error => {
                        console.error("Error deleting user:", error);
                    });
        }
      };
    
    const handleChangeName = (event: React.ChangeEvent<HTMLInputElement>) => {
        setUser(prevUser => ({...prevUser, name: event.target.value}));
    }

    const handleOnClose = () => {
        setUser({name: ""});
        onClose();
    }
    return (
        <Modal
            open={open}
            onClose={handleOnClose}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description"
        >
            <Box sx={style}>
                <Typography id="modal-modal-title" variant="h6" component="h2">
                    {props.user ? "Edytuj" : "Dodaj"} użytkownika
                </Typography>
                <TextField type="text" variant='outlined' placeholder="Imię" onChange={handleChangeName} value={user.name}/>
                <Button onClick={handleClickSave} variant="contained" color="success">
                    {props.user ? "Zapisz" : "Dodaj"}
                </Button>
            </Box>
        </Modal>
    );
}

export default UserModal;