import React, { useState, useEffect } from "react";
import axios from "axios";
import {
  Button,
  Card,
  CardBody,
  CardFooter,
  Col,
  Container,
  Form,
  Input,
  InputGroup,
  Row,
} from "reactstrap";

function EditBook(props) {
 
  const [book, setBook] = useState({
    book_id: "",
    title: "",
    author: "",
    description: "",
    description2: "",
  });
  console.log(book);

  useEffect(() => {
    getBookList();
  }, []);

  function getBookList() {
    const GetData = async () => {
      const result = await axios(
        "http://localhost:57356/api/v1/books/GetBookList"
      );
   const data = result.data.callBackObj.books;
   let book = {};
   if(data && data.length > 0){
    book = data.find(x => x.book_id == props.match.params.id);
   }
      setBook(book);
      console.log('edBook',book);
    };

    GetData();
  }

  const UpdateBook = (e) => {
    e.preventDefault();
    if (book.title === "" || book.author === "") {
      alert("please enter book title and author name");
    }
    const data = {
      mode: "Edit",
      book_id: props.match.params.id,
      title: book.title,
      author: book.author,
      description: book.description,
      description2: book.description2,
    };
    console.log("edit", data);
    axios
      .post("http://localhost:57356/api/v1/books/InsertBookList", data)
      .then((result) => {
        console.log("che", result.data);
        console.log("test", result.setBook);
        props.history.push("/BookList");

        setBook(result);
      });
  };

  const onChange = (e) => {
    e.persist();
    setBook({ ...book, [e.target.name]: e.target.value });
  };

  return (
    <div className="app flex-row align-items-center">
      <Container>
        <Row className="justify-content-center">
          <Col md="12" lg="10" xl="8">
            <Card className="mx-4">
              <CardBody className="p-4">
                <Form onSubmit={UpdateBook}>
                  <h1>Update Book</h1>
                  <InputGroup className="mb-3">
                    <Input
                      type="text"
                      name="title"
                      id="title"
                      placeholder="Title"
                      value={book.title}
                      onChange={onChange}
                    />
                  </InputGroup>
                  <InputGroup className="mb-3">
                    <Input
                      type="text"
                      name="author"
                      id="author"
                      placeholder="author"
                      value={book.author}
                      onChange={onChange}
                    />
                  </InputGroup>
                  <InputGroup className="mb-3">
                    <Input
                      type="textarea"
                      name="description"
                      id="description"
                      placeholder="Description"
                      value={book.description}
                      onChange={onChange}
                    />
                  </InputGroup>
                  <InputGroup className="mb-3">
                    <Input
                      type="textarea"
                      name="description2"
                      id="description2"
                      placeholder="Comments"
                      value={book.description2}
                      onChange={onChange}
                    />
                  </InputGroup>
                  <CardFooter className="p-4">
                    <Row>
                      <Col xs="12" sm="6">
                        <Button
                          type="submit"
                          className="btn btn-info mb-1"
                          block
                        >
                          <span>Save</span>
                        </Button>
                      </Col>
                      <Col xs="12" sm="6">
                        <Button className="btn btn-info mb-1" block>
                          <span>Cancel</span>
                        </Button>
                      </Col>
                    </Row>
                  </CardFooter>
                </Form>
              </CardBody>
            </Card>
          </Col>
        </Row>
      </Container>
    </div>
  );
}

export default EditBook;
