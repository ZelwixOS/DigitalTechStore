import React from 'react';
import Button from '@material-ui/core/Button';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import { Grid, Snackbar, TextField } from '@material-ui/core';
import { Alert } from '@mui/material';

import { createCommonCategory } from 'src/Requests/PostRequests';

interface IRefresher {
  refresh: () => void;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    spaces: {
      margin: theme.spacing(2),
    },
  }),
);

interface ICreateCommonCategory {
  refresher?: IRefresher;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const CreateCommonCategory: React.FC<ICreateCommonCategory> = props => {
  const classes = useStyles();

  const [commonCategoryData, setCommonCategoryData] = React.useState({ name: '', description: '' });

  const [open, setOpen] = React.useState<boolean>(false);
  const [message, setMessage] = React.useState<string>('');

  const handleNameChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    setCommonCategoryData({
      name: event.target.value as string,
      description: commonCategoryData.description,
    });
  };

  const handleDescriptionChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    setCommonCategoryData({
      name: commonCategoryData.name,
      description: event.target.value as string,
    });
  };

  const handleClose = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };

  const onClick = async () => {
    if (commonCategoryData.name.length < 2) {
      setMessage('Введите корректное название');
      setOpen(true);
    } else if (commonCategoryData.description.length < 5) {
      setMessage('Введите корректное описание');
      setOpen(true);
    } else {
      const res = await createCommonCategory(commonCategoryData.name, commonCategoryData.description);
      if (res && props.refresher) {
        props.refresher.refresh();
        props.setOpen(false);
      } else {
        setMessage('Не удалось выполнить запрос');
        setOpen(true);
      }
    }
  };

  return (
    <Grid container direction="column" justifyContent="center">
      <Snackbar
        anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
        open={open}
        autoHideDuration={6000}
        onClose={handleClose}
      >
        <Alert onClose={handleClose} severity="warning">
          {message}
        </Alert>
      </Snackbar>
      <TextField
        id="commonCategoryName"
        className={classes.spaces}
        value={commonCategoryData.name}
        onChange={handleNameChange}
        label="Название"
        variant="outlined"
      />
      <TextField
        id="commonCategoryDescription"
        className={classes.spaces}
        value={commonCategoryData.description}
        onChange={handleDescriptionChange}
        label="Описание"
        variant="outlined"
      />
      <Grid container justifyContent="flex-end">
        <Button type="submit" className={classes.spaces} color="primary" variant="contained" onClick={onClick}>
          Создать
        </Button>
      </Grid>
    </Grid>
  );
};

export default CreateCommonCategory;
