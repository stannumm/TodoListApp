export function DeleteData(type) {
    let BaseURL = 'https://localhost:44351/';

    return new Promise((resolve, reject) =>{
    
         
        fetch(BaseURL+type, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                },
          })
          .then((res) => {
            resolve(res);
          })
          .catch((error) => {
            reject(error);
          });

  
      });
}