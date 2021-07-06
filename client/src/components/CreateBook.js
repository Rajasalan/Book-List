import React, { useState } from "react";
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

function CreateBook(props) {
  const [book, setBook] = useState({
    book_id: "",
    title: "",
    author: "",
    description: "",
    description2: "",
  });

  const apiUrl = "http://localhost:57356/api/v1/books/InsertBookList";

  const InsertBook = (e) => {
    e.preventDefault();
    //debugger;
    if (book.title === "" || book.author === "") {
      alert("please enter book title and author name");
    }

    const data = {
      mode: "Create",
      title: book.title,
      author: book.author,
      description: book.description,
      description2: book.description2,
    };

    axios.post(apiUrl, data).then((result) => {
      console.log("data", data);
      console.log("result", result);
      props.history.push("/BookList");
      //setBook(result)
    });
  };

  const onChange = (e) => {
    e.persist();
    // debugger;
    setBook({ ...book, [e.target.name]: e.target.value });
  };
  return (
    <div className="app flex-row align-items-center">
      <Container>
        <Row className="justify-content-center">
          <Col md="12" lg="10" xl="8">
            <Card className="mx-4">
              <CardBody className="p-4">
                <Form onSubmit={InsertBook}>
                  <h1>Add Book Details</h1>
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
                      placeholder="Author"
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

export default CreateBook;
