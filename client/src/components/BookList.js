import React, { useState, useEffect } from "react";
import { CardBody, CardHeader, Col, Row, Table } from "reactstrap";
import axios from "axios";


function BookList(props) {
  const [data, setData] = useState([]);
  const [book, setBook] = useState({
    book_id: "",
    title: "",
    author: "",
    description: "",
    description2: "",
  });

  useEffect(() => {
    getBookList();
  }, []);

  function getBookList() {
    const GetData = async () => {
      const result = await axios(
        "http://localhost:57356/api/v1/books/GetBookList"
      );

      setData(result.data.callBackObj.books);
    };

    GetData();
  }
  const deleteBook = (id) => {
    const data = {
      mode: "Delete",
      book_id: id,
      title: book.title,
      author: book.author,
      description: book.description,
      description2: book.description2,
    };
    axios
      .post("http://localhost:57356/api/v1/books/InsertBookList", data)

      .then((result) => {
        console.log("delete", result);
        getBookList();
        props.history.push("/BookList");
      });
  };

  const editBook = (id) => {
    props.history.push({
      pathname: "/edit/" + id,
    });
  };

  return (
    <div className="animated fadeIn">
      <Row>
        <Col>
          <CardHeader><h2>
            <i className="fa fa-book fa-1x" aria-hidden="true"> <br/></i> My Book
            Collection </h2>
          </CardHeader>
          <CardBody>
            <Table hover bordered striped responsive size="sm">
              <thead>
                <tr>
                  <th>
                    <h5>Title</h5>
                  </th>
                  <th>
                    <h5>Author</h5>
                  </th>
                  <th>
                    <h5>Description</h5>
                  </th>
                  <th>
                    <h5> Comments </h5>
                  </th>
                </tr>
              </thead>
              <tbody>
                {data.map((item, id) => {
                  return (
                    <tr key={item.book_id}>
                      <td>
                        <h6>{item.title}</h6>
                      </td>
                      <td>
                        <h6>{item.author}</h6>
                      </td>
                      <td>{item.description}</td>
                      <td>{item.description2}</td>
                      <td>
                        <div className="btn-group">
                          <button
                            className="btn btn-warning"
                            style={{ backgroundColor: "#FEEB00" }}
                            onClick={() => {
                              editBook(item.book_id);
                            }}
                          >
                            <i
                              className="fa fa-pencil-square-o fa-2x"
                              aria-hidden="true"
                            ></i>
                          </button>
                          <button
                            className="btn btn-warning"
                            style={{ backgroundColor: '#F70000' }}
                            onClick={() => {
                              deleteBook(item.book_id);
                            }}
                          >
                            <i
                              className="fa fa-trash fa-2x"
                              aria-hidden="true"
                            ></i>
                          </button>
                        </div>
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </Table>
         
          </CardBody>
        </Col>
      </Row>
    
    </div>
  );
}

export default BookList;
