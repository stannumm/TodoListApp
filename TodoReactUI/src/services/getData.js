export function GetData(type) {
    let BaseURL = 'https://localhost:44351/';

    return new Promise((resolve, reject) =>{
    
         
        fetch(BaseURL+type, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                },
          })
          .then((response) => response.json())
          .then((res) => {
            resolve(res);
          })
          .catch((error) => {
            reject(error);
          });

  
      });
}