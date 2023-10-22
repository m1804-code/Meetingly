import { DataGrid, GridColDef, GridRemoveIcon } from "@mui/x-data-grid";
import { Configuration, ScheduledDate, User, UsersApi } from "../../../client";
import { Fab } from "@mui/material";
import EditIcon from '@mui/icons-material/Edit';

export interface UsersTableProps {
    users: User[]
    handleEditUser: (user: User) => void;
    setUsers: React.Dispatch<React.SetStateAction<User[]>>;
}

const UsersTable: React.FC<UsersTableProps> = ({ users, setUsers, handleEditUser, ...props }) => {
        const columns: GridColDef[] = [
            { field: 'id', headerName: 'ID', width: 150 },
            { field: 'name', headerName: 'Name', width: 150 },
            { field: 'scheduledDates', headerName: 'Scheduled Dates', width: 150,
            valueGetter: (params) => (params.row.scheduledDates as ScheduledDate[]).length},
            {
            field: 'actions',
            headerName: 'Actions',
            width: 150,
            renderCell: (params) => (
                <div style={{display: "flex", gap: "5px"}}>
                <Fab color="error" size="small" aria-label="add" onClick={() => deleteUser(params.row.id)}>
                    <GridRemoveIcon />
                </Fab>
                <Fab color="secondary" size="small" aria-label="edit" onClick={() => handleEditUser(params.row)}>
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
    return (
            <>
                <div>
                    <DataGrid rows={users} columns={columns} />
                </div>
            </>
        )
};

export default UsersTable;