use editor;

CREATE TABLE document (
  id INT IDENTITY(1,1) PRIMARY KEY,
  title VARCHAR(255) NOT NULL,
  description TEXT NOT NULL
);
